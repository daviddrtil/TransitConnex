using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class RouteSchedulingTemplateRepository(AppDbContext db) :
    BaseRepository<RouteSchedulingTemplate, RouteSchedulingTemplateUpdateCommand>(db),
    IRouteSchedulingTemplateRepository
{
    public IQueryable<RouteSchedulingTemplate> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
