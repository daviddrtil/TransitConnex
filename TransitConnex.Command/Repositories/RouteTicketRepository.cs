using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.RouteTicket;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
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
}
