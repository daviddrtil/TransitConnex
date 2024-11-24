using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteTicketCommandHandler(IRouteTicketService routeTicketService)
    : IBaseCommandHandler<IRouteTicketCommand>
{
    private readonly IRouteTicketService _routeTicketService = routeTicketService;

    public async Task<Guid> HandleCreate(IRouteTicketCommand command)
    {
        if (command is not RouteTicketCreateCommand routeTicketCreateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteTicketCommand.");
        }

        return new Guid();
    }

    public async Task HandleUpdate(IRouteTicketCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleDelete(IRouteTicketCommand command)
    {
        throw new NotImplementedException();
    }
}
