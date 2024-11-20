using MongoDB.Driver;
using TransitConnex.Domain.Collections.Interfaces;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

/// <summary>
/// Base repository class for read-only operations.
/// </summary>
/// <typeparam name="TQueryModel">The type of the query model.</typeparam>
/// <typeparam name="Tkey">The type of the key.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="BaseRepository{TQueryModel, Tkey}"/> class.
/// </remarks>
/// <param name="context">The read database context.</param>
internal abstract class BaseRepository<TQueryModel, Tkey>(IReadDbContext context)
    : IBaseRepository<TQueryModel, Tkey>
    where TQueryModel : IQueryModel<Tkey>
    where Tkey : IEquatable<Tkey>
{
    protected readonly IMongoCollection<TQueryModel> Collection = context.GetCollection<TQueryModel>();

    /// <summary>
    /// Gets a query model by its id.
    /// </summary>
    /// <param name="id">The id of the query model.</param>
    /// <returns>The query model.</returns>
    public async Task<TQueryModel> GetByIdAsync(Tkey id)
    {
        return await Collection
            .Find(queryModel => queryModel.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves all query models from the collection.
    /// </summary>
    /// <returns>Enumerable of query models.</returns>
    public async Task<IEnumerable<TQueryModel>> GetAllAsync()
    {
        return await Collection
            .Find(_ => true)
            .Limit(5)       // todo delete - only for testing
            .ToListAsync();
    }

    /// <summary>
    /// Upserts a query model. If it exists, it will be updated; otherwise, a new document will be inserted.
    /// </summary>
    /// <param name="queryModel">The query model to upsert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpsertAsync(TQueryModel queryModel)
    {
        var filter = Builders<TQueryModel>.Filter.Eq(q => q.Id, queryModel.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        await Collection.ReplaceOneAsync(filter, queryModel, options);
    }

    /// <summary>
    /// Deletes a query model by its ID.
    /// </summary>
    /// <param name="id">The ID of the query model to delete.</param>
    /// <returns>True whether document was deleted, otherwise false.</returns>
    public async Task<bool> DeleteAsync(Tkey id)
    {
        var filter = Builders<TQueryModel>.Filter.Eq(queryModel => queryModel.Id, id);
        var result = await Collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
