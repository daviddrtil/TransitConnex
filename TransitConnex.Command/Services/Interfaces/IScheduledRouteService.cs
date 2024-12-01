using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IScheduledRouteService
{
    Task<IEnumerable<ScheduledRouteDto>> GetAll();
    Task<ScheduledRouteDto> GetAllById(Guid id);

    Task<IEnumerable<ScheduledRoute>> GetAllByRouteId(Guid routeId);
    Task<IEnumerable<ScheduledRoute>> GetAllByStopId(Guid stopId);
    Task<IEnumerable<ScheduledRoute>> GetAllByIds(IEnumerable<Guid> ids);

    Task<ScheduledRoute> CreateScheduledRoute(ScheduledRouteCreateCommand createCommand);
    Task<ScheduledRoute> EditScheduledRoute(ScheduledRouteUpdateCommand editCommand);
    Task DeleteScheduledRoute(Guid id);
}
