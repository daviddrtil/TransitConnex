using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class StopService(IMapper mapper, IStopRepository stopRepository) : IStopService
{
    public Task<List<StopDto>> GetAllStops()
    {
        throw new NotImplementedException();
    }

    public Task<StopDto> GetStopById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> StopExists(Guid id)
    {
        throw new NotImplementedException();
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
        
        // TODO -> include locations or place into separate endpoint?
        stop = mapper.Map(editCommand, stop);
        await stopRepository.Update(stop);
        
        return stop;
    }

    public Task DeleteStop(Guid id) // TODO -> very complicated operation -> mby soft delete into hard delete?
    {
        throw new NotImplementedException(); 
    }
}
