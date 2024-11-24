using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class VehicleRepository(AppDbContext db) : BaseRepository<Vehicle, VehicleUpdateCommand>(db), IVehicleRepository
{
    // todo QueryAll probably retrive all data and filter it in memory not on sql
    public IQueryable<Vehicle> QueryById(Guid vehicleId)
    {
        return QueryAll().Where(v => v.Id == vehicleId);
    }
}
