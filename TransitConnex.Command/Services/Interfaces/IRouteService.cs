using TransitConnex.Domain.DTOs.Route;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteService
{
    Task<List<RouteDto>> GetAllRoutes();

    Task<RouteDto> GetRouteById(Guid id);

    Task<bool> RouteExists(Guid id);

    Task<RouteDto> CreateRoute(RouteCreateDto routeDto);

    Task<RouteDto> EditRoute(Guid id, RouteCreateDto editedRoute);

    Task DeleteRoute(Guid id);
}
