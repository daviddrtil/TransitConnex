using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Vehicle;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle, VehicleUpdateCommand>
    {
        IQueryable<Vehicle> QueryById(Guid vehicleId);
    }
}
