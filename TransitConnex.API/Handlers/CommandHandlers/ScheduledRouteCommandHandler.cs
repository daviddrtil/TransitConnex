using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class ScheduledRouteCommandHandler(
    IScheduledRouteService srService,
    IScheduledRouteMongoService srMongoService)
    : IBaseCommandHandler<IScheduledRouteCommand>
{
    public async Task<Guid> HandleCreate(IScheduledRouteCommand command)
    {
        if (command is not ScheduledRouteCreateCommand createCommand)
        {
            throw new InvalidCastException();
        }
        var scheduled = await srService.CreateScheduledRoute(createCommand);
        await srMongoService.Create(scheduled);
        return scheduled.Id;
    }

    public async Task HandleUpdate(IScheduledRouteCommand command)
    {
        if (command is not ScheduledRouteUpdateCommand editCommand)
        {
            throw new InvalidCastException();
        }
        var scheduled = await srService.EditScheduledRoute(editCommand);
        await srMongoService.Update(scheduled);
    }

    public async Task HandleDelete(Guid id)
    {
        await srService.DeleteScheduledRoute(id);
        await srMongoService.Delete(id);
    }
}
