using Microsoft.EntityFrameworkCore;
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
}
