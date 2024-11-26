using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IScheduledRouteRepository : IBaseRepository<ScheduledRoute, ScheduledRouteUpdateCommand>
{
    IQueryable<ScheduledRoute> QueryById(Guid id);

    // Task<bool> Exists(Guid id);
}
