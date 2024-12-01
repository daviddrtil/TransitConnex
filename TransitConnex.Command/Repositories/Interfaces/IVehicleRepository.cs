using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IVehicleRepository : IBaseRepository<Vehicle, VehicleUpdateCommand>
{
    IQueryable<Vehicle> QueryById(Guid vehicleId);
    
    IQueryable<ScheduledRoute> QueryByVehicleScheduledRoutes(Guid vehicleId);
    
    Task AddServicesToVehicle(List<VehicleOfferedService> vehicleServices);
}
