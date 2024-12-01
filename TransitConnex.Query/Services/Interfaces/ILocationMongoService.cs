using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface ILocationMongoService
{
    Task<IEnumerable<LocationDto>> GetAll();
    Task<LocationDto?> GetById(Guid id);
    Task<IEnumerable<LocationDto>> GetByName(string name);
    Task<LocationDto?> GetClosest(double latitude, double longitude);
    Task<Guid> Create(Location location);
    Task Update(Location location);
    Task Delete(Guid id);

    Task<IEnumerable<Guid>> Create(IEnumerable<Location> locations);
    Task Update(IEnumerable<Location> locations);
    Task Delete(IEnumerable<Guid> ids);
}
