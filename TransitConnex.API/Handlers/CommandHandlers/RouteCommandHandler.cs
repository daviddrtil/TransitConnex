using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteCommandHandler(
    IRouteService routeService,
    IScheduledRouteService srService,
    IScheduledRouteMongoService srMongoService)
        : IBaseCommandHandler<IRouteCommand>
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
        var srs = await srService.GetAllByRouteId(editCommand.Id);
        await srMongoService.Update(srs);
    }

    public async Task HandleDelete(Guid id)
    {
        var srs = await srService.GetAllByRouteId(id);  // has to be done before deleting route
        var srIds = srs.Select(sr => sr.Id);

        await routeService.DeleteRoute(id);

        await srMongoService.Delete(srIds);
    }
    
    public async Task HandleAddStopToRoute(IRouteCommand command)
    {
        if(command is not RouteStopAddCommand stopRouteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopRouteCommand.");
        }
        await routeService.AddStopToRoute(stopRouteCommand);
        var srs = await srService.GetAllByRouteId(stopRouteCommand.RouteId);
        await srMongoService.Update(srs);
    }
    
    public async Task HandleRemoveStopFromRoute(IRouteCommand command)
    {
        if(command is not RouteStopRemoveCommand stopRouteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopRouteCommand.");
        }
        await routeService.RemoveStopFromRoute(stopRouteCommand);
        var srs = await srService.GetAllByRouteId(stopRouteCommand.RouteId);
        await srMongoService.Update(srs);
    }
}
