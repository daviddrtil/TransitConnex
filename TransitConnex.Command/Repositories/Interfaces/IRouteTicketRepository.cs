using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IRouteTicketRepository : IBaseRepository<RouteTicket, RouteTicketUpdateCommand>
{
    IQueryable<RouteTicket> QueryById(Guid id);
}
