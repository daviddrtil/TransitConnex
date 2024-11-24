using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class RouteRepository : BaseRepository<Route, RouteUpdateCommand>, IRouteRepository
{
    private readonly AppDbContext _db;

    public RouteRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Route> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
