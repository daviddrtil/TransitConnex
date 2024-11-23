using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class ScheduledRouteMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<ScheduledRouteDoc, Guid>(readDbContext), IScheduledRouteMongoRepository
{
}
