using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IScheduledRouteRepository : IBaseRepository<ScheduledRoute, ScheduledRouteUpdateCommand>
{
    IQueryable<ScheduledRoute> QueryById(Guid id);
    IQueryable<ScheduledRoute> QueryAllScheduledRoutes();

    Task UpsertBatch(List<ScheduledRoute> scheduledRoutes);
    // Task<bool> Exists(Guid id);
}
