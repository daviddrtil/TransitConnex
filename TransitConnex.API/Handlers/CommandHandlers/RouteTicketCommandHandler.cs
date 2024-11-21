using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.RouteTicket;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class RouteTicketCommandHandler : IBaseCommandHandler<IRouteTicketCommand>
    {
        private readonly IRouteTicketService _routeTicketService;

        public RouteTicketCommandHandler(IRouteTicketService routeTicketService)
        {
            _routeTicketService = routeTicketService;
        }
            
        public Task HandleCreate(IRouteTicketCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IRouteTicketCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IRouteTicketCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
