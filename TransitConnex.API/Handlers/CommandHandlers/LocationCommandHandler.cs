using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class LocationCommandHandler(
    ILocationService locationService,
    ILocationMongoService locationMongoService)
        : IBaseCommandHandler<ILocationCommand>
{
    public async Task<Guid> HandleCreate(ILocationCommand command)
    {
        if (command is not LocationCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(LocationCreateCommand)}.");
        }
        var created = await locationService.CreateLocation(createCommand);
        await locationMongoService.Create(created);
        return created.Id;
    }

    public async Task<List<Guid>> HandleBatchCreate(ILocationCommand command)
    {
        if (command is not LocationBatchCreateCommand createCommands)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(LocationBatchCreateCommand)}.");
        }
        var created = await locationService.CreateLocations(createCommands.Locations);
        await locationMongoService.Create(created);
        return created.Select(x => x.Id).ToList();
    }

    public async Task HandleUpdate(ILocationCommand command)
    {
        if (command is not LocationUpdateCommand updateCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(LocationUpdateCommand)}.");
        }
        var location = await locationService.EditLocation(updateCommand);
        await locationMongoService.Update(location);
    }

    public async Task HandleDelete(Guid id)
    {
        await locationService.DeleteLocation(id);
        await locationMongoService.Delete(id);
    }
}
