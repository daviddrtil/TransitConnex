using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class RouteTicketRepository : BaseRepository<RouteTicket, RouteTicketUpdateCommand>, IRouteTicketRepository
{
    private readonly AppDbContext _db;

    public RouteTicketRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<RouteTicket> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
