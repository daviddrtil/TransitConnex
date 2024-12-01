using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class LocationRepository : BaseRepository<Location, LocationUpdateCommand>, ILocationRepository
{
    private readonly AppDbContext _db;

    public LocationRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

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
        return _db.LocationStops.Where(x => x.LocationId == locationId && x.StopId == stopId);
    }

    public async Task AddStopToLocation(LocationStop locationStop)
    {
        _db.LocationStops.Add(locationStop);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveStopFromLocation(LocationStop locationStop)
    {
        _db.LocationStops.Remove(locationStop);
        await _db.SaveChangesAsync();
    }
}
