using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface ILocationMongoService
{
    Task<IEnumerable<Location>> GetAll();
    Task<Location?> GetById(Guid id);
    Task<Guid> Create(Location location);
    Task Update(Location location);
    Task Delete(Guid id);
}
