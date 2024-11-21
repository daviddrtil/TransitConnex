using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public class RouteSchedulingTemplateSeed
    {
        public static void Seed(AppDbContext context)
        {
            var routeSchedulingTemplatesToBeSeeded = new List<RouteSchedulingTemplate>() { };        
        }
    }
}
