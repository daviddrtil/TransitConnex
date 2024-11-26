using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IRouteStopMongoRepository : IBaseMongoRepository<RouteStopDoc, Guid>
{
}
