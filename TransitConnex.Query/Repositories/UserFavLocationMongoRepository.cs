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

    public async Task<bool> Delete(UserFavLocationDoc location)
    {
        var filter = Builders<UserFavLocationDoc>.Filter.And(
            Builders<UserFavLocationDoc>.Filter.Eq(r => r.UserId, location.UserId),
            Builders<UserFavLocationDoc>.Filter.Eq(r => r.LocationId, location.LocationId)
        );
        var result = await Collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
