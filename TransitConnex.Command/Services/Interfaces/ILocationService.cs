using TransitConnex.Domain.DTOs.Location;

namespace TransitConnex.Infrastructure.Services.Interfaces;

public interface ILocationService
{
    Task<List<LocationDto>> GetAllLocations();

    Task<LocationDto> GetLocationById(Guid id);

    Task<bool> LocationExists(Guid id);

    Task<LocationDto> CreateLocation(LocationCreateDto locationDto);

    Task<LocationDto> EditLocation(Guid id, LocationCreateDto editedLocation);

    Task DeleteLocation(Guid id);
}
