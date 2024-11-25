using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class UserFavLocationMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<UserFavLocationDoc, Guid>(readDbContext), IUserFavLocationMongoRepository
{
    public async Task<IEnumerable<UserFavLocationDoc>> GetByUserId(Guid userId)
    {
        return await Collection
            .Find(l => l.UserId == userId)
            .SortByDescending(l => l.AddTime)
            .ToListAsync();
    }
}
