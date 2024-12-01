using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class RouteTicketRepository(AppDbContext db)
    : BaseRepository<RouteTicket, RouteTicketUpdateCommand>(db), IRouteTicketRepository
{
    public IQueryable<RouteTicket> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
