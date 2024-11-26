using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class DbSeeder
{
    public static async Task SeedAll(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Asynchronous seeding
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        await UserSeed.Seed(userManager, roleManager);

        // Synchronous seeding
        IconSeed.Seed(context);
        ServiceSeed.Seed(context);
        
        LocationSeed.Seed(context);
        LineSeed.Seed(context);
        StopSeed.Seed(context);
        RouteSeed.Seed(context);
        RouteSchedulingTemplateSeed.Seed(context);
        VehicleSeed.Seed(context);
        SeatSeed.Seed(context);
        ScheduledRouteSeed.Seed(context);
        RouteTicketSeed.Seed(context);
    }
}
