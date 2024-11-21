using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Location;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class LocationRepository : BaseRepository<Location, LocationUpdateCommand>, ILocationRepository
    {
        private readonly AppDbContext _db;

        public LocationRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Location> QueryById(Guid id)
        {
            return QueryAll().Where(x => x.Id == id);
        }
    }
}
