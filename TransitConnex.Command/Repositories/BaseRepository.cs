using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class BaseRepository<T, U> : IBaseRepository<T, U> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
            _appDbContext = context;
        }

        public IQueryable<T> QueryAll(bool asNoTracking = true)
        {
            return asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
        }
        
        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Update(T entity, U updatedEntity) // TODO check this -> what happens when null given or with FKs ?
        {
            foreach (var property in typeof(U).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var modelProperty = typeof(T).GetProperty(property.Name);
                if (modelProperty != null && modelProperty.CanWrite && modelProperty.Name != "Id")
                {
                    var updatedValue = property.GetValue(updatedEntity);
                    modelProperty.SetValue(entity, updatedValue);
                }
            }
            
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
        
        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
