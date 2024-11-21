using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.RouteTicket;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IRouteTicketRepository : IBaseRepository<RouteTicket, RouteTicketUpdateCommand>
    {
        IQueryable<RouteTicket> QueryById(Guid id);
    }
}
