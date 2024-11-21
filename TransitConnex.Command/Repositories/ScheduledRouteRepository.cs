using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.ScheduledRoute;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class ScheduledRouteRepository : BaseRepository<ScheduledRoute, ScheduledRouteUpdateCommand>, IScheduledRouteRepository
    {
        private readonly AppDbContext _db;

        public ScheduledRouteRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<ScheduledRoute> QueryById(Guid id)
        {
            return QueryAll().Where(x => x.Id == id);
        }
    }
}
