using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public class RouteSeed
    {
        public static void Seed(AppDbContext context)
        {
            var routesToBeSeeded = new List<Route>()
            {
                new()
                {
                    Name = "",
                    
                }
            };
        }
    }
}
