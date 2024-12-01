using EFCore.BulkExtensions;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class RouteRepository(AppDbContext db) : BaseRepository<Route, RouteUpdateCommand>(db), IRouteRepository
{
    private readonly AppDbContext Db = db;

    public IQueryable<Route> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public IQueryable<RouteStop> QueryRoutePath(Guid routeId)
    {
        return Db.RouteStops.Where(x => x.RouteId == routeId).OrderBy(x => x.StopOrder);
    }

    public IQueryable<RouteStop> QueryRouteStops(Guid? routeId, Guid? stopId)
    {
        var query = Db.RouteStops.AsQueryable();

        if (routeId != null)
        {
            query = query.Where(x => x.RouteId == routeId);
        }

        if (stopId != null)
        {
            query = query.Where(x => x.StopId == stopId);
        }
        
        return query;
    }

    public async Task AddRouteStops(List<RouteStop> routeStops)
    {
        Db.RouteStops.AddRange(routeStops);
        await Db.SaveChangesAsync();
    }

    public async Task UpdateBatchRouteStops(List<RouteStop> routeStops)
    {
        Db.RouteStops.UpdateRange(routeStops);
        await Db.SaveChangesAsync();
    }

    public async Task UpsertBatchRouteStops(List<RouteStop> routeStops)
    {
        await Db.BulkInsertOrUpdateAsync(routeStops);
    }

    public async Task DeleteRouteStop(RouteStop routeStop)
    {
        Db.RouteStops.Remove(routeStop);
        await Db.SaveChangesAsync();
    }
    
    public async Task DeleteRouteStops(List<RouteStop> routeStops)
    {
        Db.RouteStops.RemoveRange(routeStops);
        await Db.SaveChangesAsync();
    }
}
