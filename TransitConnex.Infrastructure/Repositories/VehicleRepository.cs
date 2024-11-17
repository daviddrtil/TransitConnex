using Microsoft.EntityFrameworkCore;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        private readonly AppDbContext _db;
        
        public VehicleRepository(
            AppDbContext db
        ) : base(db)
        {
            _db = db;
        }

        public IQueryable<Vehicle> QueryById(Guid vehicleId)
        {
            return QueryAll().Where(v => v.Id == vehicleId);
        }
    }
}
