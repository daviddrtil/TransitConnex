using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IScheduledRouteService
{
    Task<List<ScheduledRouteDto>> GetAllScheduledRoutes();
    Task<ScheduledRouteDto> GetScheduledRouteById(Guid id);

    Task<ScheduledRoute> CreateScheduledRoute(ScheduledRouteCreateCommand createCommand);
    Task<ScheduledRoute> EditScheduledRoute(ScheduledRouteUpdateCommand editCommand);
    Task DeleteScheduledRoute(Guid id);
}
