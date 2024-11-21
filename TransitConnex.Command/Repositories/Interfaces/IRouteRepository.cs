using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Route;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IRouteRepository : IBaseRepository<Route, RouteUpdateCommand>
    {
        IQueryable<Route> QueryById(Guid id);
    }
}
