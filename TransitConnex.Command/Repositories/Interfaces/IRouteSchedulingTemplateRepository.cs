using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IRouteSchedulingTemplateRepository : IBaseRepository<RouteSchedulingTemplate>
    {
        IQueryable<RouteSchedulingTemplate> QueryById(Guid id);
    }
}
