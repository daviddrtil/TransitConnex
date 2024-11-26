using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteTicketCommandHandler(IRouteTicketService routeTicketService)
    : IBaseCommandHandler<IRouteTicketCommand>
{
    public async Task<Guid> HandleCreate(IRouteTicketCommand command)
    {
        if (command is not RouteTicketCreateCommand routeTicketCreateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteTicketCommand.");
        }
        
        var created  = await routeTicketService.CreateRouteTicket(routeTicketCreateCommand);

        return created.Id;
    }

    public Task HandleUpdate(IRouteTicketCommand command)
    {
        return Task.CompletedTask;
    }

    public Task HandleDelete(IRouteTicketCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleDelete(Guid id)
    {
        await routeTicketService.DeleteRouteTicket(id);
    }
}
