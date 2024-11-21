using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public class ScheduledRouteSeed
    {
        public static void Seed(AppDbContext context)
        {
            var ScheduledRoutesToBeSeeded = new List<ScheduledRoute>() { };
        }
    }
}
