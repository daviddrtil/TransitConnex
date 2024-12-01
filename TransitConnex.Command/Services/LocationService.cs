using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Command.Services;

public class LocationService(
    IMapper mapper, 
    ILocationRepository locationRepository, 
    IUserRepository userRepository
    ) : ILocationService
{
    public async Task<List<LocationDto>> GetLocationsFiltered(LocationFilteredQuery filter)
    {
        var query = locationRepository.QueryAll();

        if (filter.Name != null)
        {
            query = query.Where(location => location.Name != null && location.Name.Contains(filter.Name));
        }

        if (filter.LocationType != null)
        {
            query = query.Where(x => x.LocationType == filter.LocationType);
        }
        
        var locations = await query.ToListAsync();
        return mapper.Map<List<LocationDto>>(locations);
    }
    
    public async Task<Location?> GetLocationById(Guid id)
    {
        return await locationRepository
            .QueryById(id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Location>> GetLocationByIds(IEnumerable<Guid> ids)
    {
        return await locationRepository
            .QueryAllLocations()
            .Where(l => ids.Contains(l.Id))
            .ToListAsync();
    }

    public async Task<Location> CreateLocation(LocationCreateCommand createCommand)
    {
        var newLocationObj = mapper.Map<Location>(createCommand);
        await locationRepository.Add(newLocationObj);

        // Load all properties
        var newLocation = await locationRepository
            .QueryById(newLocationObj.Id)
            .AsNoTracking()
            .FirstAsync();

        return newLocation;
    }

    public async Task<List<Location>> CreateLocations(List<LocationCreateCommand> createCommands)
    {
        var newLocationObjs = mapper.Map<List<Location>>(createCommands);
        await locationRepository.AddBatch(newLocationObjs);

        var locationIds = newLocationObjs.Select(l => l.Id).ToList();
        var newLocations = await locationRepository
            .QueryAllLocations()
            .Where(l => locationIds.Contains(l.Id))
            .AsNoTracking()
            .ToListAsync();

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

        var favouriteConnections = await userRepository.QueryAllUserConnectionFavouritesWithLocationId(id).ToListAsync();
        await userRepository.DeleteUserConnectionFavourites(favouriteConnections);
        
        await locationRepository.Delete(location);
    }
}
