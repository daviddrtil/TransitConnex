using MongoDB.Driver.Linq;
using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public class ScheduledRouteSeed
{
    private static readonly DateTime InitDate = DateTime.Parse("2024-11-14");
    private static readonly Vehicle TrainVehicle = VehicleSeed.Vehicles
        .Where(v => v.VehicleType == VehicleTypeEnum.TRAIN)
        .First();
    private static readonly List<Route> TrainRoutes = RouteSeed.Routes
        .Where(r => r.Name != null && r.Name.Contains("R8"))
        .ToList();

    public static readonly List<ScheduledRoute> ScheduledRoutes = [
        new()
        {
            Id = Guid.Parse("81ab86a0-03c8-417f-845e-54fbcf3db92e"),
            VehicleId = TrainVehicle.Id,
            StartTime = InitDate,
            EndTime = InitDate.Add(TrainRoutes[0].DurationTime),
            RouteId = TrainRoutes[0].Id,
            Price = 100
        },
        new()
        {
            Id = Guid.Parse("83cb1e22-77bc-40be-8a7b-79408b56edd6"),
            VehicleId = TrainVehicle.Id,
            StartTime = InitDate.AddHours(1),
            EndTime = InitDate.AddHours(1).Add(TrainRoutes[0].DurationTime),
            RouteId = TrainRoutes[0].Id,
            Price = 100
        }
    ];

    public static void Seed(AppDbContext context)
    {
        foreach (var scheduledRoute in ScheduledRoutes)
        {
            context.ScheduledRoutes.Add(scheduledRoute);
        }
        context.SaveChanges();
    }
}
