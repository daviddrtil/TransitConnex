using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface
    IRouteSchedulingTemplateRepository : IBaseRepository<RouteSchedulingTemplate, RouteSchedulingTemplateUpdateCommand>
{
    IQueryable<RouteSchedulingTemplate> QueryById(Guid id);
}
