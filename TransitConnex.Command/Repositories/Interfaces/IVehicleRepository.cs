using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        IQueryable<Vehicle> QueryById(Guid vehicleId);
    }
}
