using TransitConnex.Domain.Collections.Interfaces;

namespace TransitConnex.Query.Repositories.Interfaces;

/// <summary>
///     Represents a read-only repository interface.
/// </summary>
/// <typeparam name="TQueryModel">The type of the query model.</typeparam>
/// <typeparam name="TKey">The type of the key for the query model.</typeparam>
public interface IBaseMongoRepository<TQueryModel, in TKey>
    where TQueryModel : IQueryModel<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    ///     Gets the query model by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the query model.</param>
    /// <returns>The task representing the asynchronous operation, returning the query model.</returns>
    Task<TQueryModel?> GetById(TKey id);

    /// <summary>
    ///     Retrieves all query models from the collection.
    /// </summary>
    /// <returns>Enumerable of query models.</returns>
    Task<IEnumerable<TQueryModel>> GetAll();

    /// <summary>
    ///     Upserts a query model. If it exists, it will be updated; otherwise, a new document will be inserted.
    /// </summary>
    /// <param name="queryModel">The query model to upsert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Upsert(TQueryModel queryModel);

    /// <summary>
    ///     Upserts a collection of query models in a single batch operation.
    /// </summary>
    /// <param name="queryModels">The collection of query models to upsert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Upsert(IEnumerable<TQueryModel> queryModels);

    /// <summary>
    ///     Deletes a query model by its ID.
    /// </summary>
    /// <param name="id">The ID of the query model to delete.</param>
    /// <returns>True whether document was deleted, otherwise false.</returns>
    Task<bool> Delete(TKey id);

    /// <summary>
    ///     Deletes a collection of query models by their IDs in a single batch operation.
    /// </summary>
    /// <param name="ids">The collection of IDs of the query models to delete.</param>
    /// <returns>True if any document was deleted, otherwise false</returns>
    Task<bool> Delete(IEnumerable<TKey> ids);
}
