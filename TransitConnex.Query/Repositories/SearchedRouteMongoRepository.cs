using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class SearchedRouteMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<SearchedRouteDoc, Guid>(readDbContext), ISearchedRouteMongoRepository
{
    public async Task<IEnumerable<SearchedRouteDoc>> GetByUserId(Guid userId)
    {
        var filter = Builders<SearchedRouteDoc>.Filter.Eq(x => x.UserId, userId);
        return await Collection
            .Find(filter)
            .Limit(10)
            .ToListAsync();
    }
}
