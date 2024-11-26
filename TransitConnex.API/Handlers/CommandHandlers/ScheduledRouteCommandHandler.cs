using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class ScheduledRouteCommandHandler(IScheduledRouteService scheduledRouteService)
    : IBaseCommandHandler<IScheduledRouteCommand>
{
    public async Task<Guid> HandleCreate(IScheduledRouteCommand command)
    {
        if (command is not ScheduledRouteCreateCommand createCommand)
        {
            throw new InvalidCastException();
        }
        
        var scheduled = await scheduledRouteService.CreateScheduledRoute(createCommand);

        // TODO -> sync
        
        return scheduled.Id;
    }

    public async Task HandleUpdate(IScheduledRouteCommand command)
    {
        if (command is not ScheduledRouteUpdateCommand editCommand)
        {
            throw new InvalidCastException();
        }
        
        var scheduled = await scheduledRouteService.EditScheduledRoute(editCommand);
        // TODO -> sync
    }

    public async Task HandleDelete(Guid id)
    {
        await scheduledRouteService.DeleteScheduledRoute(id);
        
        // TODO -> sync
    }
}
