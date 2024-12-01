using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class StopRepository(AppDbContext db) : BaseRepository<Stop, StopUpdateCommand>(db), IStopRepository
{
    public IQueryable<Stop> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public IQueryable<RouteStop> QueryRouteStopsByStopId(Guid stopId)
    {
        return db.RouteStops.Where(x => x.StopId == stopId).AsNoTracking();
    }

    public async Task AddLocationStops(List<LocationStop> locationStops)
    {
        db.LocationStops.AddRange(locationStops);
        await db.SaveChangesAsync();
    }
}
