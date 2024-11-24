using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteCommandHandler(IRouteService routeService) : IBaseCommandHandler<IRouteCommand>
{
    private readonly IRouteService _routeService = routeService;

    public async Task<Guid> HandleCreate(IRouteCommand command)
    {
        if (command is not RouteCreateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteCreateCommand.");
        }

        return new Guid();
    }

    public async Task HandleUpdate(IRouteCommand command)
    {
        if (command is not RouteUpdateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteUpdateCommand.");
        }
    }

    public async Task HandleDelete(IRouteCommand command)
    {
        if (command is not RouteDeleteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteDeleteCommand.");
        }
    }
}
