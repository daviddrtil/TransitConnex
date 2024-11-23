using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IScheduledRouteMongoRepository : IBaseMongoRepository<ScheduledRouteDoc, Guid>
{
}
