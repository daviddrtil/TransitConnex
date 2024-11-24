using TransitConnex.Command.Commands.Route;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IRouteRepository : IBaseRepository<Route, RouteUpdateCommand>
{
    IQueryable<Route> QueryById(Guid id);
}
