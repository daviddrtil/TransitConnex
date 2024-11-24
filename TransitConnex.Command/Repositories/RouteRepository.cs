using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Route;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories;

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
