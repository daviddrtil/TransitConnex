using TransitConnex.Command.Commands.Route;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IRouteRepository : IBaseRepository<Route, RouteUpdateCommand>
{
    IQueryable<Route> QueryById(Guid id);
    IQueryable<RouteStop> QueryRoutePath(Guid routeId);
    IQueryable<RouteStop> QueryRouteStops(Guid? routeId, Guid? stopId);

    Task AddRouteStops(List<RouteStop> routeStops);
    Task UpdateBatchRouteStops(List<RouteStop> routeStops);
    Task UpsertBatchRouteStops(List<RouteStop> routeStops);
    Task DeleteRouteStop(RouteStop routeStop);
    Task DeleteRouteStops(List<RouteStop> routeStops);
}
