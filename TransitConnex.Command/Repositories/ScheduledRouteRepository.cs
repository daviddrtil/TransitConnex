using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class ScheduledRouteRepository : BaseRepository<ScheduledRoute, ScheduledRouteUpdateCommand>,
    IScheduledRouteRepository
{
    private readonly AppDbContext _db;

    public ScheduledRouteRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<ScheduledRoute> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
