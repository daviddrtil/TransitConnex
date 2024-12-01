using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class LocationRepository(AppDbContext db)
    : BaseRepository<Location, LocationUpdateCommand>(db), ILocationRepository
{
    private readonly AppDbContext Db = db;

    private IQueryable<Location> QueryLocations()
    {
        return QueryAll()
            .Include(l => l.Stops);
    }

    public IQueryable<Location> QueryById(Guid id)
    {
        return QueryLocations().Where(x => x.Id == id);
    }

    public IQueryable<Location> QueryAllLocations()
    {
        return QueryLocations();
    }

    public IQueryable<LocationStop> QueryLocationStop(Guid locationId, Guid stopId)
    {
        return Db.LocationStops.Where(x => x.LocationId == locationId && x.StopId == stopId);
    }

    public async Task AddStopToLocation(LocationStop locationStop)
    {
        Db.LocationStops.Add(locationStop);
        await Db.SaveChangesAsync();
    }

    public async Task RemoveStopFromLocation(LocationStop locationStop)
    {
        Db.LocationStops.Remove(locationStop);
        await Db.SaveChangesAsync();
    }
}
