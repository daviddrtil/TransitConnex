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
                           ?? throw new ApplicationException("Missing ConnectionStrings:NoSqlConnection");
        bool isTestEnv = Environment.GetEnvironmentVariable("AppEnviroment") == "Test";
        if (isTestEnv)
        {
            ConnectionString += "_test";
        }

        var mongoUrl = MongoUrl.Create(ConnectionString);
        var client = new MongoClient(mongoUrl);
        _database = client.GetDatabase(mongoUrl.DatabaseName);
        _logger = logger;
    }
    public string ConnectionString { get; }

    #endregion


    #region CreateDeleteCollections

    public async Task CreateCollectionsAsync()
    {
        using var asyncCursor = await _database.ListCollectionNamesAsync();
        var collections = await asyncCursor.ToListAsync();

        var collectionNamesFromAssembly = GetCollectionNamesFromAssembly();
        foreach (string collectionName in collectionNamesFromAssembly)
        {
            if (collectionName == nameof(RouteStopDoc))
            {
                // Skip nested object
                continue;
            }
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
    }

    public async Task DeleteCollectionsAsync()
    {
        using var asyncCursor = await _database.ListCollectionNamesAsync();
        var collections = await asyncCursor.ToListAsync();

        var collectionNamesFromAssembly = GetCollectionNamesFromAssembly();
        foreach (string collectionName in collectionNamesFromAssembly)
        {
            if (collections.Exists(db => db.Equals(collectionName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _logger.LogInformation("----- MongoDB: deleting the Collection {Name}", collectionName);

                await _database.DropCollectionAsync(collectionName);
            }
            else
            {
                _logger.LogInformation("----- MongoDB: the {Name} collection does not exists", collectionName);
            }
        }
    }

    #endregion CreateDeleteCollections

    #region CreateIndexes
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

    private async Task CreateLocationSpatialIndexAsync()
    {
        // Define the index key for geospatial queries
        string indexKey = "coordinates";

        // Create the index definition for a 2dsphere index
        var indexDefinition = Builders<LocationDoc>.IndexKeys
            .Geo2DSphere(indexKey);

        // Create the index model
        var indexModel = new CreateIndexModel<LocationDoc>(indexDefinition,
            new CreateIndexOptions { Unique = false });

        // Get the collection
        var collection = GetCollection<LocationDoc>();

        // Check if the index already exists
        var existingIndexes = await collection.Indexes.ListAsync();
        var indexesList = await existingIndexes.ToListAsync();
        bool indexExists = indexesList.Any(index =>
            index["key"].AsBsonDocument.Contains(indexKey) &&
            index["key"].AsBsonDocument[indexKey] == "2dsphere");

        // If the index doesn't exist, create it
        if (!indexExists)
        {
            string indexName = await collection.Indexes.CreateOneAsync(indexModel);
            _logger.LogInformation("----- MongoDB: Location spatial index successfully created - {indexName}", indexName);
        }
    }

    private async Task CreateLocationNameIndexAsync()
    {
        string indexField = nameof(LocationDoc.Name);
        var collection = GetCollection<LocationDoc>(); // Replace with your collection accessor

        // Define an ascending index on the "Name" field
        var indexDefinition = Builders<LocationDoc>.IndexKeys.Ascending(indexField);
        var indexModel = new CreateIndexModel<LocationDoc>(indexDefinition);

        // Check if the index already exists
        var existingIndexes = await collection.Indexes.ListAsync();
        var indexesList = await existingIndexes.ToListAsync();

        bool indexExists = indexesList.Any(index =>
            index["key"].AsBsonDocument.Contains(indexField));

        if (!indexExists)
        {
            // Create the index
            string indexName = await collection.Indexes.CreateOneAsync(indexModel);
            _logger.LogInformation("----- MongoDB: Index on '{IndexField}' successfully created - {IndexName}", indexField, indexName);
        }
        else
        {
            _logger.LogInformation("----- MongoDB: Index on '{IndexField}' already exists", indexField);
        }
    }

    private async Task CreateUserFavLocationIndexesAsync()
    {
        string indexKey1 = "userId";
        string indexKey2 = "locationId";
        string indexKey3 = "addTime";

        // Define the index
        var indexDefinition = Builders<UserFavLocationDoc>.IndexKeys
            .Ascending(indexKey1)
            .Ascending(indexKey2)
            .Descending(indexKey3);

        var indexModel = new CreateIndexModel<UserFavLocationDoc>(indexDefinition, DefaultCreateIndexOptions);
        var collection = GetCollection<UserFavLocationDoc>();

        // Check if the index already exists
        var existingIndexes = await collection.Indexes.ListAsync();
        var indexesList = await existingIndexes.ToListAsync();
        bool indexExists = indexesList.Any(index =>
            index["key"].AsBsonDocument.Contains(indexKey1)
            && index["key"].AsBsonDocument.Contains(indexKey2)
            && index["key"].AsBsonDocument.Contains(indexKey3));

        // Create the index if it doesn't exist
        if (!indexExists)
        {
            string indexName = await collection.Indexes.CreateOneAsync(indexModel);
            _logger.LogInformation("----- MongoDB: UserFavLocation indexes successfully created - {indexName}", indexName);
        }
    }

    private async Task CreateUserFavConnectionIndexesAsync()
    {
        string indexKey1 = "userId";
        string indexKey2 = "fromLocationId";
        string indexKey3 = "toLocationId";
        string indexKey4 = "addTime";

        // Define the index
        var indexDefinition = Builders<UserFavConnectionDoc>.IndexKeys
            .Ascending(indexKey1)
            .Ascending(indexKey2)
            .Ascending(indexKey3)
            .Descending(indexKey4);

        var indexModel = new CreateIndexModel<UserFavConnectionDoc>(indexDefinition, DefaultCreateIndexOptions);
        var collection = GetCollection<UserFavConnectionDoc>();

        // Check if the index already exists
        var existingIndexes = await collection.Indexes.ListAsync();
        var indexesList = await existingIndexes.ToListAsync();
        bool indexExists = indexesList.Any(index =>
            index["key"].AsBsonDocument.Contains(indexKey1)
            && index["key"].AsBsonDocument.Contains(indexKey2)
            && index["key"].AsBsonDocument.Contains(indexKey3)
            && index["key"].AsBsonDocument.Contains(indexKey4));

        // Create the index if it doesn't exist
        if (!indexExists)
        {
            string indexName = await collection.Indexes.CreateOneAsync(indexModel);
            _logger.LogInformation("----- MongoDB: UserFavConnection indexes successfully created - {indexName}", indexName);
        }
    }

    private async Task CreateSearchedRouteIndexesAsync()
    {
        string indexKey1 = "userId";

        // Define the index
        var indexDefinition = Builders<SearchedRouteDoc>.IndexKeys
            .Ascending(indexKey1);

        var indexModel = new CreateIndexModel<SearchedRouteDoc>(indexDefinition);
        var collection = GetCollection<SearchedRouteDoc>();

        // Check if the index already exists
        var existingIndexes = await collection.Indexes.ListAsync();
        var indexesList = await existingIndexes.ToListAsync();
        bool indexExists = indexesList.Any(index =>
            index["key"].AsBsonDocument.Contains(indexKey1));

        // Create the index if it doesn't exist
        if (!indexExists)
        {
            string indexName = await collection.Indexes.CreateOneAsync(indexModel);
            _logger.LogInformation("----- MongoDB: UserFavLocation indexes successfully created - {indexName}", indexName);
        }
    }

    public async Task CreateIndexAsync()
    {
        _logger.LogInformation("----- MongoDB: creating indexes...");

        await CreateVehicleRTIIndexesAsync();

        await CreateLocationNameIndexAsync();
        await CreateLocationSpatialIndexAsync();

        await CreateUserFavLocationIndexesAsync();
        await CreateUserFavConnectionIndexesAsync();
        await CreateSearchedRouteIndexesAsync();

        _logger.LogInformation("----- MongoDB: indexes successfully created");
    }
    #endregion CreateIndexes

    #region GetCollectionNames
    public IMongoCollection<TQueryModel> GetCollection<TQueryModel>() where TQueryModel : IQueryModel
    {
        return _database.GetCollection<TQueryModel>(typeof(TQueryModel).Name);
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
    #endregion GetCollectionNames

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
