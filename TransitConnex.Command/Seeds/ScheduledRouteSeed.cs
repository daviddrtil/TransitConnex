using MongoDB.Driver.Linq;
using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class ScheduledRouteSeed
{
    public static void Seed(AppDbContext context)
    {
        var vehicles = context.Vehicles.Where(v => v.VehicleType == VehicleTypeEnum.TRAIN).ToList();
        var routes = context.Routes.Where(r => r.Name != null && r.Name.Contains("R8")).ToList();
        var initDate = DateTime.Parse("2024-11-14");
        
        var scheduledRoutesToBeSeeded = new List<ScheduledRoute>()
        {
            new()
            {
                Id = Guid.Parse("81ab86a0-03c8-417f-845e-54fbcf3db92e"),
                VehicleId = vehicles[0].Id,
                StartTime = initDate,
                EndTime = initDate.Add(routes[0].DurationTime),
                RouteId = routes[0].Id,
                Price = 100
            },
            new()
            {
                Id = Guid.Parse("83cb1e22-77bc-40be-8a7b-79408b56edd6"),
                VehicleId = vehicles[0].Id,
                StartTime = initDate.AddHours(1),
                EndTime = initDate.AddHours(1).Add(routes[0].DurationTime),
                RouteId = routes[0].Id,
                Price = 100
            }
        };

        foreach (var scheduledRoute in scheduledRoutesToBeSeeded)
        {
            context.ScheduledRoutes.Add(scheduledRoute);
        }
        
        context.SaveChanges();
    }
}
