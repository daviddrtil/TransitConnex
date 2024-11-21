using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.RouteSchedulingTemplate;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IRouteSchedulingTemplateRepository : IBaseRepository<RouteSchedulingTemplate, RouteSchedulingTemplateUpdateCommand>
    {
        IQueryable<RouteSchedulingTemplate> QueryById(Guid id);
    }
}
