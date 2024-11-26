using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class RouteStopMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<RouteStopDoc, Guid>(readDbContext), IRouteStopMongoRepository
{
}
