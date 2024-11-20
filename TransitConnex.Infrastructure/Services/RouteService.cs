using TransitConnex.Domain.DTOs.Route;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public Task<List<RouteDto>> GetAllRoutes()
        {
            throw new NotImplementedException();
        }

        public Task<RouteDto> GetRouteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RouteExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RouteDto> CreateRoute(RouteCreateDto routeDto)
        {
            throw new NotImplementedException();
        }

        public Task<RouteDto> EditRoute(Guid id, RouteCreateDto editedRoute)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoute(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
