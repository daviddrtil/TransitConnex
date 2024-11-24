using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Vehicle;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories;

public class VehicleRepository(AppDbContext db) : BaseRepository<Vehicle, VehicleUpdateCommand>(db), IVehicleRepository
{
    // todo QueryAll probably retrive all data and filter it in memory not on sql
    public IQueryable<Vehicle> QueryById(Guid vehicleId)
    {
        return QueryAll().Where(v => v.Id == vehicleId);
    }
}
