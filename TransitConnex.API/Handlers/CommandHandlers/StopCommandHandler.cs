using Microsoft.IdentityModel.Tokens;
using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class StopCommandHandler(
    IStopService stopService,
    ILocationService locationService,
    ILocationMongoService locationMongoService,
    IScheduledRouteService srService,
    IScheduledRouteMongoService srMongoService)
        : IBaseCommandHandler<IStopCommand>
{
    /// <summary>
    /// // Sync MongoDb location collection
    /// </summary>
    /// <param name="stop"></param>
    /// <returns></returns>
    private async Task SyncLocationCache(Stop stop)
    {
        if (!stop.LocationStops.IsNullOrEmpty())
        {
            var locationIds = stop.LocationStops!.Select(x => x.LocationId).Distinct();
            var locations = await locationService.GetLocationByIds(locationIds);
            if (!locations.IsNullOrEmpty())
                await locationMongoService.Update(locations);
        }
    }

    private async Task SyncScheduledRouteCache(Stop stop)
    {
        var srs = await srService.GetAllByStopId(stop.Id);
        await srMongoService.Update(srs);
    }

    public async Task<Guid> HandleCreate(IStopCommand command)
    {
        if (command is not StopCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(StopCreateCommand)}.");
        }
        var stop = await stopService.CreateStop(createCommand);
        await SyncLocationCache(stop);
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
        await SyncLocationCache(stop);
        await SyncScheduledRouteCache(stop);
    }

    public async Task HandleDelete(Guid id)
    {
        var stop = await stopService.GetStopById(id)
            ?? throw new KeyNotFoundException($"Stop with ID: {id} not found.");
        await stopService.DeleteStop(id);
        await SyncLocationCache(stop);
        await SyncScheduledRouteCache(stop);
    }

    public async Task HandleAddStopToLocation(IStopCommand command)
    {
        if (command is not StopLocationCommand stopCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopLocationCommand.");
        }
        await stopService.AssignStopToLocation(stopCommand);

        var location = await locationService.GetLocationById(stopCommand.LocationId);
        if (location is not null)
            await locationMongoService.Update(location);
    }
    
    public async Task HandleRemoveStopFromLocation(IStopCommand command)
    {
        if (command is not StopLocationCommand stopCommand)
        {
            throw new InvalidCastException("Invalid command given, expected StopLocationCommand.");
        }
        await stopService.RemoveStopFromLocation(stopCommand);

        var location = await locationService.GetLocationById(stopCommand.LocationId);
        if (location is not null)
            await locationMongoService.Update(location);
    }
}
