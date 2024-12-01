using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface IScheduledRouteService
{
    Task<IEnumerable<ScheduledRoute>> GetAllByRouteId(Guid routeId);
    Task<IEnumerable<ScheduledRoute>> GetAllByStopId(Guid stopId);
    Task<IEnumerable<ScheduledRoute>> GetAllByIds(IEnumerable<Guid> ids);
    Task<List<ScheduledRouteDto>> GetScheduledRoutesFiltered(ScheduledRouteFilteredQuery filter);

    Task<ScheduledRoute> CreateScheduledRoute(ScheduledRouteCreateCommand createCommand);
    Task<ScheduledRoute> EditScheduledRoute(ScheduledRouteUpdateCommand editCommand);
    Task DeleteScheduledRoute(Guid id);
}
