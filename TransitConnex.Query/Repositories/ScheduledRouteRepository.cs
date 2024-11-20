using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

internal class ScheduledRouteRepository(IReadDbContext readDbContext)
    : BaseRepository<ScheduledRouteDoc, Guid>(readDbContext), IScheduledRouteRepository
{
}
