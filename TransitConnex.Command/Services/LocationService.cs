using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class LocationService(
    IMapper mapper, 
    ILocationRepository locationRepository, 
    IUserRepository userRepository
    ) : ILocationService
{
    public Task<List<LocationDto>> GetAllLocations()
    {
        throw new NotImplementedException();
    }

    public Task<List<LocationDto>> GetLocationsFiltered()
    {
        throw new NotImplementedException();
    }

    public Task<LocationDto> GetLocationById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Location> CreateLocation(LocationCreateCommand createCommand)
    {
        var newLocation = mapper.Map<Location>(createCommand);
        await locationRepository.Add(newLocation);
        
        return newLocation;
    }

    public async Task<List<Location>> CreateLocations(List<LocationCreateCommand> createCommands)
    {
        var newLocations = mapper.Map<List<Location>>(createCommands);
        await locationRepository.AddBatch(newLocations);

        return newLocations;
    }

    public async Task<Location> EditLocation(LocationUpdateCommand editCommand)
    {
        var location = await locationRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();
        if (location == null)
        {
            throw new KeyNotFoundException($"Location with ID: {editCommand.Id} not found.");
        }
        
        location = mapper.Map(editCommand, location);
        await locationRepository.Update(location);
        
        return location;
    }

    public async Task DeleteLocation(Guid id)
    {
        var location = await locationRepository.QueryById(id).FirstAsync();

        if (location == null)
        {
            throw new KeyNotFoundException($"Location with ID: {id} was not found.");
        }
        
        // TODO -> how to handle stops??

        var favouriteConnections = await userRepository.QueryAllUserConnectionFavouritesWithLocationId(id).ToListAsync();
        await userRepository.DeleteUserConnectionFavourites(favouriteConnections);
        
        await locationRepository.Delete(location);
    }
}
