using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class StopCommandHandler(IStopService stopService) : IBaseCommandHandler<IStopCommand>
{
    public async Task<Guid> HandleCreate(IStopCommand command)
    {
        if (command is not StopCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(StopCreateCommand)}.");        }

        var created = await stopService.CreateStop(createCommand);

        return created.Id;
    }

    public async Task<List<Guid>> HandleBatchCreate(IStopCommand command)
    {
        if (command is not StopBatchCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(StopBatchCreateCommand)}.");
        }
        
        var created = await stopService.CreateStops(createCommand.Stops);

        return created.Select(x => x.Id).ToList();
    }

    public async Task HandleUpdate(IStopCommand command)
    {
        if (command is not StopUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopUpdateCommand.");
        }

        await stopService.EditStop(updateCommand);
    }

    public async Task HandleDelete(Guid id)
    {
        await stopService.DeleteStop(id);
    }
}
