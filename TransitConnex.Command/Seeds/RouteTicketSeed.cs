using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public class RouteTicketSeed
    {
        public static void Seed(AppDbContext context)
        {
            var routeTicketsToBeSeeded = new List<RouteTicket>() { };
        }
    }
}
