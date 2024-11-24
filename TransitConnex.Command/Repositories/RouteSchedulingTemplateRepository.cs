using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

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
