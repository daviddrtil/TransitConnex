using TransitConnex.Domain.DTOs.ScheduledRoute;

namespace TransitConnex.Infrastructure.Services.Interfaces;

public interface IScheduledRouteService
{
    Task<List<ScheduledRouteDto>> GetAllScheduledRoutes();

    Task<ScheduledRouteDto> GetScheduledRouteById(Guid id);

    Task<bool> ScheduledRouteExists(Guid id);

    Task<ScheduledRouteDto> CreateScheduledRoute(ScheduledRouteCreateDto scheduledRouteDto);

    Task<ScheduledRouteDto> EditScheduledRoute(Guid id, ScheduledRouteCreateDto editedScheduledRoute);

    Task DeleteScheduledRoute(Guid id);
}
