namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T, U>
    {
        IQueryable<T> QueryAll(bool asNoTracking = true);
        
        Task Add(T entity);
        Task Update(T entity, U updatedEntity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> Exists(Guid id);
    }
}
