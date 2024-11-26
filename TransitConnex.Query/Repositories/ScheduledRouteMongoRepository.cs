using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class ScheduledRouteMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<ScheduledRouteDoc, Guid>(readDbContext), IScheduledRouteMongoRepository
{
    public async Task<IEnumerable<ScheduledRouteDoc>> GetAll(
        IEnumerable<Guid> startStopIds, IEnumerable<Guid> endStopIds, DateTime startTime)
    {
        var filter = Builders<ScheduledRouteDoc>.Filter.And(
            // Ensure at least one of the start stop IDs matches and departureTime > startTime
            Builders<ScheduledRouteDoc>.Filter.ElemMatch(
                "stops",
                Builders<Stop>.Filter.And(
                    Builders<Stop>.Filter.In("_id", startStopIds),
                    Builders<Stop>.Filter.Gt("departureTime", startTime.ToUniversalTime())
                )
            ),
            // Ensure at least one of the end stop IDs matches
            Builders<ScheduledRouteDoc>.Filter.ElemMatch(
                "stops",
                Builders<Stop>.Filter.In("_id", endStopIds)
            )
        );
        var sort = Builders<ScheduledRouteDoc>.Sort.Ascending("stops.departureTime");
        return await Collection
            .Find(filter)
            .Sort(sort)
            .Limit(10)
            .ToListAsync();
    }
}
