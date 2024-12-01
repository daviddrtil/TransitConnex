using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class StopService(IMapper mapper, IRouteService routeService, IStopRepository stopRepository, ILocationRepository locationRepository) : IStopService
{
    public async Task<Stop?> GetStopById(Guid id)
    {
        return await stopRepository.QueryById(id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<List<StopDto>> GetFilteredStops(StopFilteredQuery filter)
    {
        var query = stopRepository.QueryAll();

        if (filter.Ids != null)
        {
            query = query.Where(s => filter.Ids.Contains(s.Id));
        }

        if (filter.Name != null)
        {
            query = query.Where(s => s.Name != null && s.Name.Contains(filter.Name));
        }

        if (filter.LocationId != null)
        {
            query = query.Where(x => x.LocationStops != null && x.LocationStops.Any(l => l.LocationId == filter.LocationId));
        }

        if (filter.RouteId != null)
        {
            query = query.Where(s => s.RouteStops != null && s.RouteStops.Any(rs => rs.RouteId == filter.RouteId));
        }
        
        var stops = await query.ToListAsync();
        
        return mapper.Map<List<StopDto>>(stops);
    }

    public async Task<Stop> CreateStop(StopCreateCommand createCommand)
    {
        var newStop = mapper.Map<Stop>(createCommand);
        await stopRepository.Add(newStop);

        var locationStops = createCommand.LocationIds
            .Select(
                locationId => new LocationStop
                {
                    LocationId = locationId, 
                    StopId = newStop.Id
                }
            )
            .ToList();
        await stopRepository.AddLocationStops(locationStops);
        
        return newStop;
    }

    public async Task<List<Stop>> CreateStops(List<StopCreateCommand> createCommands)
    {
        var stops = new List<Stop>();
        var locationStops = new List<LocationStop>();
        foreach (var createCommand in createCommands)
        {
            var newStop = mapper.Map<Stop>(createCommand);
            stops.Add(newStop);
            locationStops.AddRange(
                createCommand.LocationIds
                    .Select(
                        locationId => new LocationStop {LocationId = locationId, StopId = newStop.Id}
                    )
                    .ToList());
        }
        
        await stopRepository.AddBatch(stops);
        await stopRepository.AddLocationStops(locationStops);
        
        return stops;
    }

    public async Task<Stop> EditStop(StopUpdateCommand editCommand)
    {
        var stop = await stopRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();
        if (stop == null)
        {
            throw new KeyNotFoundException($"Stop with ID: {editCommand.Id} not found.");
        }
        
        stop = mapper.Map(editCommand, stop);
        await stopRepository.Update(stop);
        
        return stop;
    }

    public async Task AssignStopToLocation(StopLocationCommand addCommand)
    {
        var stop = await stopRepository.QueryById(addCommand.StopId).FirstOrDefaultAsync();
        if (stop == null)
        {
            throw new KeyNotFoundException($"Stop with ID: {addCommand.StopId} not found.");
        }
        
        var location = await locationRepository.QueryById(addCommand.LocationId).FirstOrDefaultAsync();
        if (location == null)
        {
            throw new KeyNotFoundException($"Location with ID: {addCommand.LocationId} not found.");
        }

        var newLocationStop = mapper.Map<LocationStop>(addCommand);
        await locationRepository.AddStopToLocation(newLocationStop);
    }

    public async Task RemoveStopFromLocation(StopLocationCommand removeCommand)
    {
        var locationStop = await locationRepository.QueryLocationStop(removeCommand.LocationId, removeCommand.StopId).FirstOrDefaultAsync();
        if (locationStop == null)
        {
            throw new KeyNotFoundException($"Given relation between stop and location was not found. Locations ID: {removeCommand.LocationId}, Stop ID: {removeCommand.StopId}.");
        }
        
        await locationRepository.RemoveStopFromLocation(locationStop);
    }

    public async Task DeleteStop(Guid id)
    {
        var stop = await stopRepository.QueryById(id).Include(x => x.RouteStops).FirstOrDefaultAsync();
        if (stop == null)
        {
            throw new KeyNotFoundException($"Stop with ID: {id} not found.");
        }

        var routeStops = await stopRepository.QueryRouteStopsByStopId(stop.Id).ToListAsync();
        
        foreach (var routeStop in routeStops)
        {
            await routeService.RemoveStopFromRoute(new RouteStopRemoveCommand()
            {
                RouteId = routeStop.RouteId, StopId = routeStop.StopId
            });
        }
        
        stop = await stopRepository.QueryById(id).Include(x => x.RouteStops).FirstOrDefaultAsync();
        await stopRepository.Delete(stop!);
    }
}
