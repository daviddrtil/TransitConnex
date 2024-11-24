using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class ScheduledRouteCommandHandler(IScheduledRouteService scheduledRouteService)
    : IBaseCommandHandler<IScheduledRouteCommand>
{
    public async Task<Guid> HandleCreate(IScheduledRouteCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleUpdate(IScheduledRouteCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleDelete(IScheduledRouteCommand command)
    {
        throw new NotImplementedException();
    }
}
