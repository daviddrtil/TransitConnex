using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.ScheduledRoute;

namespace TransitConnex.Command.Services;

public class ScheduledRouteService(IRouteService routeService) : IScheduledRouteService
{
    public Task<List<ScheduledRouteDto>> GetAllScheduledRoutes()
    {
        throw new NotImplementedException();
    }

    public Task<ScheduledRouteDto> GetScheduledRouteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ScheduledRouteExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ScheduledRouteDto> CreateScheduledRoute(ScheduledRouteCreateDto scheduledRouteDto)
    {
        throw new NotImplementedException();
    }

    public Task<ScheduledRouteDto> EditScheduledRoute(Guid id, ScheduledRouteCreateDto editedScheduledRoute)
    {
        throw new NotImplementedException();
    }

    public Task DeleteScheduledRoute(Guid id)
    {
        throw new NotImplementedException();
    }
}
