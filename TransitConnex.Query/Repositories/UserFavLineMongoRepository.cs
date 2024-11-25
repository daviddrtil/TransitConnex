using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class UserFavLineMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<UserFavLineDoc, Guid>(readDbContext), IUserFavLineMongoRepository
{
    public async Task<IEnumerable<UserFavLineDoc>> GetByUserId(Guid userId)
    {
        return await Collection
            .Find(l => l.UserId == userId)
            .SortByDescending(l => l.AddTime)
            .ToListAsync();
    }
}
