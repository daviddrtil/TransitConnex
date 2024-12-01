using Microsoft.IdentityModel.Tokens;
using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class StopCommandHandler(
    IStopService stopService,
    ILocationService locationService,
    ILocationMongoService locationMongoService)
        : IBaseCommandHandler<IStopCommand>
{
    /// <summary>
    /// // Sync MongoDb location collection
    /// </summary>
    /// <param name="stop"></param>
    /// <returns></returns>
    private async Task SyncLocationCollection(Stop stop)
    {
        if (!stop.LocationStops.IsNullOrEmpty())
        {
            var locationIds = stop.LocationStops!.Select(x => x.LocationId).Distinct();
            var locations = await locationService.GetLocationByIds(locationIds);
            if (!locations.IsNullOrEmpty())
                await locationMongoService.Update(locations);
        }
    }

    public async Task<Guid> HandleCreate(IStopCommand command)
    {
        if (command is not StopCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(StopCreateCommand)}.");
        }
        var stop = await stopService.CreateStop(createCommand);
        await SyncLocationCollection(stop);
        return stop.Id;
    }

    public async Task<List<Guid>> HandleBatchCreate(IStopCommand command)
    {
        if (command is not StopBatchCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(StopBatchCreateCommand)}.");
        }
        var stops = await stopService.CreateStops(createCommand.Stops);

        // Sync MongoDb location collection
        var locationIds = stops
            .Where(stop => !stop.LocationStops.IsNullOrEmpty())
            .SelectMany(stop => stop.LocationStops!)
            .Select(locationStop => locationStop.LocationId)
            .ToHashSet();
        var locations = await locationService.GetLocationByIds(locationIds);
        if (!locations.IsNullOrEmpty())
            await locationMongoService.Update(locations);

        return stops.Select(x => x.Id).ToList();
    }

    public async Task HandleUpdate(IStopCommand command)
    {
        if (command is not StopUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopUpdateCommand.");
        }
        var stop = await stopService.EditStop(updateCommand);
        await SyncLocationCollection(stop);
    }

    public async Task HandleDelete(Guid id)
    {
        var stop = await stopService.GetStopById(id)
            ?? throw new KeyNotFoundException($"Stop with ID: {id} not found.");
        await stopService.DeleteStop(id);

        if (!stop.LocationStops.IsNullOrEmpty())
        {
            var locationIds = stop.LocationStops!.Select(x => x.LocationId).Distinct();
            await locationMongoService.Delete(locationIds);
        }
    }

    public async Task HandleAddStopToLocation(IStopCommand command)
    {
        if (command is not StopLocationCommand stopLocationCommandCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopLocationCommand.");
        }
        
        await stopService.AssignStopToLocation(stopLocationCommandCommand);
        // TODO -> will have to sync with mongo
    }
    
    public async Task HandleRemoveStopFromLocation(IStopCommand command)
    {
        if (command is not StopLocationCommand stopLocationCommandCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopLocationCommand.");
        }
        
        await stopService.RemoveStopFromLocation(stopLocationCommandCommand);
        // TODO -> will have to sync with mongo
    }
}
