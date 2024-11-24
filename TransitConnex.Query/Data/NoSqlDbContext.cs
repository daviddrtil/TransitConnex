using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Collections.Interfaces;
using TransitConnex.Query.Abstraction;

namespace TransitConnex.Query.Data;

public sealed class NoSqlDbContext : IReadDbContext, ISynchronizeDb
{
    #region Constructor

    private static readonly ReplaceOptions DefaultReplaceOptions = new() { IsUpsert = true };

    private static readonly CreateIndexOptions DefaultCreateIndexOptions = new() { Unique = true, Sparse = true };

    private readonly IMongoDatabase _database;
    private readonly ILogger<NoSqlDbContext> _logger;

    public NoSqlDbContext(IConfiguration configuration, ILogger<NoSqlDbContext> logger)
    {
        ConnectionString = configuration.GetConnectionString("NoSqlConnection")
                           ?? throw new ApplicationException("Missing ConnectionStrings - NoSqlConnection");
        var mongoUrl = MongoUrl.Create(ConnectionString);
        var client = new MongoClient(mongoUrl);
        _database = client.GetDatabase(mongoUrl.DatabaseName);
        _logger = logger;
    }

    #endregion

    #region IReadDbContext

    public string ConnectionString { get; }

    public IMongoCollection<TQueryModel> GetCollection<TQueryModel>() where TQueryModel : IQueryModel
    {
        return _database.GetCollection<TQueryModel>(typeof(TQueryModel).Name);
    }

    public async Task CreateCollectionsAsync()
    {
        using var asyncCursor = await _database.ListCollectionNamesAsync();
        var collections = await asyncCursor.ToListAsync();

        var collectionNamesFromAssembly = GetCollectionNamesFromAssembly();
        foreach (string collectionName in collectionNamesFromAssembly)
        {
            // Check if the collection does not exist in the database
            if (!collections.Exists(db => db.Equals(collectionName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _logger.LogInformation("----- MongoDB: creating the Collection {Name}", collectionName);

                await _database.CreateCollectionAsync(collectionName,
                    new CreateCollectionOptions { ValidationLevel = DocumentValidationLevel.Strict });
            }
            else
            {
                _logger.LogInformation("----- MongoDB: the {Name} collection already exists", collectionName);
            }
        }

        await CreateIndexAsync();
    }

    private async Task CreateVehicleRTIIndexesAsync()
    {
        string indexKey1 = "vehicleId";
        string indexKey2 = "updated";
        var indexDefinition = Builders<VehicleRTIDoc>.IndexKeys
            .Ascending(indexKey1)
            .Descending(indexKey2);

        var indexModel = new CreateIndexModel<VehicleRTIDoc>(indexDefinition, DefaultCreateIndexOptions);
        var collection = GetCollection<VehicleRTIDoc>();

        var existingIndexes = await collection.Indexes.ListAsync();
        var indexesList = await existingIndexes.ToListAsync();
        bool indexExists = indexesList.Any(index =>
            index["key"].AsBsonDocument.Contains(indexKey1)
            && index["key"].AsBsonDocument.Contains(indexKey2));
        if (!indexExists)
        {
            string indexName = await collection.Indexes.CreateOneAsync(indexModel);
            _logger.LogInformation("----- MongoDB: VehicleRTI indexes successfully created - {indexName}", indexName);
        }
    }

    private async Task CreateIndexAsync()
    {
        _logger.LogInformation("----- MongoDB: creating indexes...");

        await CreateVehicleRTIIndexesAsync();

        _logger.LogInformation("----- MongoDB: indexes successfully created");
    }

    private static List<string> GetCollectionNamesFromAssembly()
    {
        return Assembly
            .GetAssembly(typeof(IQueryModel<>))! // Ensure targeting the correct assembly
            .GetTypes()
            .Where(type =>
                type.IsClass
                && !type.IsInterface
                && !type.IsAbstract
                && typeof(IQueryModel<Guid>).IsAssignableFrom(type)
            )
            .Select(type => type.Name)
            .Distinct()
            .ToList();
    }

    #endregion

    #region ISynchronizeDb

    public async Task UpsertAsync<TQueryModel>(TQueryModel queryModel,
        Expression<Func<TQueryModel, bool>> upsertFilter)
        where TQueryModel : IQueryModel
    {
        // here can be use retry polly nuget package
        var collection = GetCollection<TQueryModel>();
        await collection.ReplaceOneAsync(upsertFilter, queryModel, DefaultReplaceOptions);
    }

    public async Task DeleteAsync<TQueryModel>(Expression<Func<TQueryModel, bool>> deleteFilter)
        where TQueryModel : IQueryModel
    {
        var collection = GetCollection<TQueryModel>();
        await collection.DeleteOneAsync(deleteFilter);
    }

    #endregion
}
