using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class IconCommandHandler(IIconService iconService) : IBaseCommandHandler<IIconCommand>
{
    public async Task<Guid> HandleCreate(IIconCommand command)
    {
        if (command is not IconCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected IconCreateCommand.");
        }

        var created = await iconService.CreateIcon(createCommand);

        return created.Id;
    }

    public async Task HandleUpdate(IIconCommand command)
    {
        if (command is not IconUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected IconUpdateCommand.");
        }

        await iconService.EditIcon(updateCommand);
    }

    public async Task HandleDelete(IIconCommand command)
    {
        if (command is not IconDeleteCommand deleteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected IconDeleteCommand.");
        }

        await iconService.DeleteIcon(deleteCommand);
    }
}
