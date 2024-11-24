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

    public IQueryable<Location> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
