using Microsoft.Extensions.DependencyInjection;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public static class DbSeeder
    {
        public static void SeedAll(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // UserSeed.Seed(context);
            IconSeed.Seed(context);
            ServiceSeed.Seed(context);
            LocationSeed.Seed(context);
            LineSeed.Seed(context);
            StopSeed.Seed(context);
            RouteSeed.Seed(context);
            RouteSchedulingTemplateSeed.Seed(context);
            ScheduledRouteSeed.Seed(context);
            VehicleSeed.Seed(context);
            SeatSeed.Seed(context);
            RouteTicketSeed.Seed(context);
        }
    }
}
