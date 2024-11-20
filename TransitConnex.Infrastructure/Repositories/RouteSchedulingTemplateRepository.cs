using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class RouteSchedulingTemplateRepository : BaseRepository<RouteSchedulingTemplate>, IRouteSchedulingTemplateRepository
    {
        private readonly AppDbContext _db;

        public RouteSchedulingTemplateRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<RouteSchedulingTemplate> QueryById(Guid id)
        {
            return QueryAll().Where(x => x.Id == id);
        }
    }
}
