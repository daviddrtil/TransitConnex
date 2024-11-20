using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        private readonly AppDbContext _db;

        public RouteRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Route> QueryById(Guid id)
        {
            return QueryAll().Where(x => x.Id == id);
        }
    }
}
