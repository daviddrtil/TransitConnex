using Microsoft.Extensions.DependencyInjection;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public static class DbSeeder
    {
        public static void SeedAll(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            IconSeed.Seed(context);
            LocationSeed.Seed(context);
            VehicleSeed.Seed(context);
            // UserSeed.Seed(context);
        }
    }
}
