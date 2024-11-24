using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Stop;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class StopCommandHandler(IStopService stopService) : IBaseCommandHandler<IStopCommand>
{
    public async Task<Guid> HandleCreate(IStopCommand command)
    {
        if (command is not StopCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopCreateCommand.");
        }
        
        var created = await stopService.CreateStop(createCommand);
        
        return created.Id;
    }

    public async Task HandleUpdate(IStopCommand command)
    {
        if (command is not StopUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopUpdateCommand.");
        }
        
        await stopService.EditStop(updateCommand);
    }

    public async Task HandleDelete(IStopCommand command)
    {
        if (command is not StopDeleteCommand deleteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopDeleteCommand.");
        }
        
        await stopService.DeleteStop(deleteCommand);
    }
}
