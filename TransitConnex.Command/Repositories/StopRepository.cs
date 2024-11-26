using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class StopRepository : BaseRepository<Stop, StopUpdateCommand>, IStopRepository
{
    private readonly AppDbContext _db;

    public StopRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Stop> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public async Task AddLocationStops(List<LocationStop> locationStops)
    {
        _db.LocationStops.AddRange(locationStops);
        await _db.SaveChangesAsync();
    }
}
