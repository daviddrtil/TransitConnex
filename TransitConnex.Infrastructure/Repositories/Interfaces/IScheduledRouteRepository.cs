using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IScheduledRouteRepository : IBaseRepository<ScheduledRoute>
    {
        IQueryable<ScheduledRoute> QueryById(Guid id);
    }
}
