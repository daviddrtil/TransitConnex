using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.ScheduledRoute;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IScheduledRouteRepository : IBaseRepository<ScheduledRoute, ScheduledRouteUpdateCommand>
    {
        IQueryable<ScheduledRoute> QueryById(Guid id);
    }
}
