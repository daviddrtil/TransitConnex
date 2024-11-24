using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.RouteSchedulingTemplate;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories;

public class RouteSchedulingTemplateRepository :
    BaseRepository<RouteSchedulingTemplate, RouteSchedulingTemplateUpdateCommand>, IRouteSchedulingTemplateRepository
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
