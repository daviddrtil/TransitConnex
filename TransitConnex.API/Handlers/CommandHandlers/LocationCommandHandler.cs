using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class LocationCommandHandler(ILocationService locationService) : IBaseCommandHandler<ILocationCommand>
{
    private readonly ILocationService _locationService = locationService;

    public async Task<Guid> HandleCreate(ILocationCommand command)
    {
        if (command is not LocationCreateCommand)
        {
        }

        return new Guid();
    }

    public async Task HandleUpdate(ILocationCommand command)
    {
        if (command is not LocationUpdateCommand)
        {
        }
    }

    public async Task HandleDelete(ILocationCommand command)
    {
        if (command is not LocationDeleteCommand)
        {
        }
    }
}
