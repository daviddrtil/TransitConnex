using MongoDB.Driver;
using TransitConnex.Domain.Collections.Interfaces;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

/// <summary>
///     Base repository class for read-only operations.
/// </summary>
/// <typeparam name="TQueryModel">The type of the query model.</typeparam>
/// <typeparam name="Tkey">The type of the key.</typeparam>
/// <remarks>
///     Initializes a new instance of the <see cref="BaseMongoRepository{TQueryModel, Tkey}" /> class.
/// </remarks>
/// <param name="context">The read database context.</param>
public abstract class BaseMongoRepository<TQueryModel, Tkey>(IReadDbContext context)
    : IBaseMongoRepository<TQueryModel, Tkey>
    where TQueryModel : IQueryModel<Tkey>
    where Tkey : IEquatable<Tkey>
{
    protected readonly IMongoCollection<TQueryModel> Collection = context.GetCollection<TQueryModel>();

    /// <summary>
    ///     Gets a query model by its id.
    /// </summary>
    /// <param name="id">The id of the query model.</param>
    /// <returns>The query model.</returns>
    public async Task<TQueryModel?> GetById(Tkey id)
    {
        return await Collection
            .Find(queryModel => queryModel.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Retrieves all query models from the collection.
    /// </summary>
    /// <returns>Enumerable of query models.</returns>
    public async Task<IEnumerable<TQueryModel>> GetAll()
    {
        return await Collection
            .Find(_ => true)
            .Limit(5) // todo delete - only for testing
            .ToListAsync();
    }

    /// <summary>
    ///     Upserts a query model. If it exists, it will be updated; otherwise, a new document will be inserted.
    /// </summary>
    /// <param name="queryModel">The query model to upsert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Upsert(TQueryModel queryModel)
    {
        var filter = Builders<TQueryModel>.Filter.Eq(q => q.Id, queryModel.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        await Collection.ReplaceOneAsync(filter, queryModel, options);
    }

    /// <summary>
    ///     Upserts a collection of query models in a single batch operation.
    /// </summary>
    /// <param name="queryModels">The collection of query models to upsert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Upsert(IEnumerable<TQueryModel> queryModels)
    {
        var requests = queryModels
            .Select(queryModel =>
                new ReplaceOneModel<TQueryModel>(
                    Builders<TQueryModel>.Filter.Eq(q => q.Id, queryModel.Id),
                    queryModel)
                { IsUpsert = true }
            )
            .ToList();
        await Collection.BulkWriteAsync(requests, new BulkWriteOptions { IsOrdered = false });
    }


    /// <summary>
    ///     Deletes a query model by its ID.
    /// </summary>
    /// <param name="id">The ID of the query models to delete.</param>
    /// <returns>True whether document was deleted, otherwise false.</returns>
    public async Task<bool> Delete(Tkey id)
    {
        var filter = Builders<TQueryModel>.Filter.Eq(queryModel => queryModel.Id, id);
        var result = await Collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    /// <summary>
    ///     Deletes a collection of query models by their IDs in a single batch operation.
    /// </summary>
    /// <param name="ids">The collection of IDs of the query models to delete.</param>
    /// <returns>True if any document was deleted, otherwise false</returns>
    public async Task<bool> Delete(IEnumerable<Tkey> ids)
    {
        var requests = ids
            .Select(id => new DeleteOneModel<TQueryModel>(
                Builders<TQueryModel>.Filter.Eq(queryModel => queryModel.Id, id)))
            .ToList();
        var result = await Collection.BulkWriteAsync(requests, new BulkWriteOptions { IsOrdered = false });
        return result.DeletedCount > 0;
    }
}
