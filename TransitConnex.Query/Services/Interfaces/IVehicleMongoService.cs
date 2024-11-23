using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface IVehicleMongoService
{
    Task<IEnumerable<Vehicle>> GetAll();
    Task<Vehicle?> GetById(Guid id);
    Task<Guid> Create(Vehicle vehicle);
    Task Update(Vehicle vehicle);
    Task Delete(Guid id);
}
