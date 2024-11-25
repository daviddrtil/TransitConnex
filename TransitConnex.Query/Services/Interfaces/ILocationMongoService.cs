using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface ILocationMongoService
{
    Task<IEnumerable<Location>> GetAll();
    Task<Location?> GetById(Guid id);
    Task<IEnumerable<Location>> GetByName(string name);
    Task<Location?> GetClosest(double latitude, double longitude);
    Task<Guid> Create(Location location);
    Task Update(Location location);
    Task Delete(Guid id);

    Task<IEnumerable<Guid>> Create(IEnumerable<Location> locations);
    Task Delete(IEnumerable<Guid> ids);
}
