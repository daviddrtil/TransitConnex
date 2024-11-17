using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        IQueryable<Route> QueryById(Guid id);
    }
}
