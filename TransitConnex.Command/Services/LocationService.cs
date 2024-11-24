using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services;

public class LocationService(ILocationRepository locationRepository) : ILocationService
{
    public Task<List<LocationDto>> GetAllLocations()
    {
        throw new NotImplementedException();
    }

    public Task<LocationDto> GetLocationById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LocationExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<LocationDto> CreateLocation(LocationCreateDto locationDto)
    {
        throw new NotImplementedException();
    }

    public Task<LocationDto> EditLocation(Guid id, LocationCreateDto editedLocation)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLocation(Guid id)
    {
        throw new NotImplementedException();
    }
}
