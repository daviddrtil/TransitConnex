using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Data;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.TestSeeds.NoSqlSeeds;

public class ScheduledRouteDocSeeder(
    AppDbContext context,
    IScheduledRouteMongoService scheduledRouteService)
{
    //public static readonly List<ScheduledRouteDoc> ScheduledRoutes = [];
    //public async Task Seed()
    //{
    //    int maxScheduledRoutes = int.Min(
    //        VehicleRTIDocSeeder.RouteIds.Count,
    //        RouteStopDocSeeder.TemplateRouteStops.Count);
    //    for (int i = 0; i < maxScheduledRoutes; i++)
    //    {
    //        var selectedRouteStops = RouteStopDocSeeder.TemplateRouteStops[i];
    //        var scheduledRoute = new ScheduledRouteDoc
    //        {
    //            Id = Guid.NewGuid(),
    //            RouteId = VehicleRTIDocSeeder.RouteIds.ElementAt(i),
    //            VehicleId = faker.Random.CollectionItem(VehicleRTIDocSeeder.VehicleIds),
    //            StartTime = selectedRouteStops.First().DepartureTime,
    //            EndTime = selectedRouteStops.Last().DepartureTime,
    //            Stops = selectedRouteStops,
    //        };
    //        ScheduledRoutes.Add(scheduledRoute);
    //    }
    //    await scheduledRouteRepo.Upsert(ScheduledRoutes);
    //}

    public async Task Seed()
    {
        var scheduledRoutes = await context.ScheduledRoutes
                .Include(sr => sr.Route)
                    .ThenInclude(r => r!.Stops)
                        .ThenInclude(rs => rs.Stop)
                .ToListAsync();
        var scheduledRouteIds = await scheduledRouteService.Create(scheduledRoutes);
    }
}
