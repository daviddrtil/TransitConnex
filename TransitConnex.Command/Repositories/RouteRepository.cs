using EFCore.BulkExtensions;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class RouteRepository(AppDbContext db) : BaseRepository<Route, RouteUpdateCommand>(db), IRouteRepository
{
    public IQueryable<Route> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public IQueryable<RouteStop> QueryRoutePath(Guid routeId)
    {
        return db.RouteStops.Where(x => x.RouteId == routeId).OrderBy(x => x.StopOrder);
    }

    public IQueryable<RouteStop> QueryRouteStops(Guid? routeId, Guid? stopId)
    {
        var query = db.RouteStops.AsQueryable();

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
        db.RouteStops.AddRange(routeStops);
        await db.SaveChangesAsync();
    }

    public async Task UpdateBatchRouteStops(List<RouteStop> routeStops)
    {
        db.RouteStops.UpdateRange(routeStops);
        await db.SaveChangesAsync();
    }

    public async Task UpsertBatchRouteStops(List<RouteStop> routeStops)
    {
        await db.BulkInsertOrUpdateAsync(routeStops);
    }

    public async Task DeleteRouteStop(RouteStop routeStop)
    {
        db.RouteStops.Remove(routeStop);
        await db.SaveChangesAsync();
    }
    
    public async Task DeleteRouteStops(List<RouteStop> routeStops)
    {
        db.RouteStops.RemoveRange(routeStops);
        await db.SaveChangesAsync();
    }
}
