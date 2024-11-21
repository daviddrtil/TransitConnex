using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Route;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class RouteCommandHandler : IBaseCommandHandler<IRouteCommand>
    {
        private readonly IRouteService _routeService;

        public RouteCommandHandler(IRouteService routeService)
        {
            _routeService = routeService;
        }
        
        public Task HandleCreate(IRouteCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IRouteCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IRouteCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
