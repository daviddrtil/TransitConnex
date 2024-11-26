using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IScheduledRouteMongoRepository : IBaseMongoRepository<ScheduledRouteDoc, Guid>
{
    Task<IEnumerable<ScheduledRouteDoc>> GetAll(
        IEnumerable<Guid> startStopIds, IEnumerable<Guid> endStopIds, DateTime startTime);
}
