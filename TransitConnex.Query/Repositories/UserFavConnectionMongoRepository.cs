using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Collections.Interfaces;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class UserFavConnectionMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<UserFavConnectionDoc, Guid>(readDbContext), IUserFavConnectionMongoRepository
{
    public async Task<IEnumerable<UserFavConnectionDoc>> GetByUserId(Guid userId)
    {
        return await Collection
            .Find(l => l.UserId == userId)
            .SortByDescending(l => l.AddTime)
            .ToListAsync();
    }

    public async Task<bool> Delete(UserFavConnectionDoc connection)
    {
        var filter = Builders<UserFavConnectionDoc>.Filter.And(
            Builders<UserFavConnectionDoc>.Filter.Eq(r => r.UserId, connection.UserId),
            Builders<UserFavConnectionDoc>.Filter.Eq(r => r.FromLocationId, connection.FromLocationId),
            Builders<UserFavConnectionDoc>.Filter.Eq(r => r.ToLocationId, connection.ToLocationId)
        );
        var result = await Collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
