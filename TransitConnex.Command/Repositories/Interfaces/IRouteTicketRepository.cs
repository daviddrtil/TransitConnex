using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IRouteTicketRepository : IBaseRepository<RouteTicket>
    {
        IQueryable<RouteTicket> QueryById(Guid id);
    }
}
