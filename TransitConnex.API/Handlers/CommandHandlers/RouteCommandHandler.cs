using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteCommandHandler(IRouteService routeService) : IBaseCommandHandler<IRouteCommand>
{
    public async Task<Guid> HandleCreate(IRouteCommand command)
    {
        if (command is not RouteCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteCreateCommand.");
        }

        var created = await routeService.CreateRoute(createCommand);
        
        return created.Id;
    }

    public async Task HandleUpdate(IRouteCommand command)
    {
        if (command is not RouteUpdateCommand editCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteUpdateCommand.");
        }
        
        await routeService.EditRoute(editCommand);
    }

    public async Task HandleDelete(Guid id)
    {
        await routeService.DeleteRoute(id);
    }
    
    public async Task HandleAddStopToRoute(IRouteCommand command)
    {
        if(command is not RouteStopAddCommand stopRouteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopRouteCommand.");
        }
        
        await routeService.AddStopToRoute(stopRouteCommand);
        // TODO -> will have to sync with mongo
    }
    
    public async Task HandleRemoveStopFromRoute(IRouteCommand command)
    {
        if(command is not RouteStopRemoveCommand stopRouteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopRouteCommand.");
        }
        
        await routeService.RemoveStopFromRoute(stopRouteCommand);
        // TODO -> will have to sync with mongo
    }
}
