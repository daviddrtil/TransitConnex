namespace TransitConnex.Command.Repositories.Interfaces;

public interface IBaseRepository<T, U>
{
    IQueryable<T> QueryAll(bool asNoTracking = true);
    IQueryable<T> QueryExistingIds(IEnumerable<Guid> ids);

    Task Add(T entity);
    Task AddBatch(IEnumerable<T> entities);
    Task Update(T entity, U updatedEntity);
    Task Update(T entity);
    Task UpdateBatch(IEnumerable<T> entities);
    Task Delete(T entity);
    Task DeleteBatch(IEnumerable<T> entities);
    Task<bool> Exists(Guid id);
    Task<bool> ExistsAll(List<Guid> ids);
}
