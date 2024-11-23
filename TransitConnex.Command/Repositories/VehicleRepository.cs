using Microsoft.EntityFrameworkCore;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Vehicle;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle, VehicleUpdateCommand>, IVehicleRepository
    {
        private readonly AppDbContext _db;
        
        public VehicleRepository(
            AppDbContext db
        ) : base(db)
        {
            _db = db;
        }

        // todo QueryAll probably retrive all data and filter it in memory not on sql
        public IQueryable<Vehicle> QueryById(Guid vehicleId)
        {
            return QueryAll().Where(v => v.Id == vehicleId);
        }
    }
}
