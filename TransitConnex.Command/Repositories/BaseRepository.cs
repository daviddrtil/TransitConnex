using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;

namespace TransitConnex.Command.Repositories;

public class BaseRepository<T, U> : IBaseRepository<T, U> where T : class
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _dbSet = context.Set<T>();
        _appDbContext = context;
    }

    public IQueryable<T> QueryAll(bool asNoTracking = true)
    {
        return asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
    }

    public IQueryable<T> QueryExistingIds(IEnumerable<Guid> ids)
    {
        return _dbSet.AsNoTracking().Where(entity => ids.Contains(EF.Property<Guid>(entity, "Id")));
    }

    public async Task Add(T entity)
    {
        _dbSet.Add(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task AddBatch(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(T entity, U updatedEntity)
    {
        foreach (var property in typeof(U).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var modelProperty = typeof(T).GetProperty(property.Name);
            if (modelProperty != null && modelProperty.CanWrite && modelProperty.Name != "Id")
            {
                object? updatedValue = property.GetValue(updatedEntity);
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

    public async Task UpdateBatch(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteBatch(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _dbSet.AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async Task<bool> ExistsAll(List<Guid> ids)
    {
        if (ids.Count == 0)
        {
            return false;
        }

        var count = await _dbSet.CountAsync(e => ids.Contains(EF.Property<Guid>(e, "Id")));
        return count == ids.Count;
    }
}
