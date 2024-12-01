using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class VehicleRepository(AppDbContext db) : BaseRepository<Vehicle, VehicleUpdateCommand>(db), IVehicleRepository
{
    private readonly AppDbContext Db = db;

    public IQueryable<Vehicle> QueryById(Guid vehicleId)
    {
        return QueryAll().Where(v => v.Id == vehicleId);
    }

    public IQueryable<ScheduledRoute> QueryByVehicleScheduledRoutes(Guid vehicleId)
    {
        return Db.ScheduledRoutes.Where(v => v.Id == vehicleId);
    }

    public async Task AddServicesToVehicle(List<VehicleOfferedService> vehicleServices)
    {
        Db.VehicleServices.AddRange(vehicleServices);
        await Db.SaveChangesAsync();
    }
}
