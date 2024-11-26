using Bogus;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Seeds;

public class ScheduledRouteDocSeeder(
    Faker faker,
    IScheduledRouteMongoRepository scheduledRouteRepo)
{
    public static readonly List<ScheduledRouteDoc> ScheduledRoutes = [];

    public async Task Seed()
    {
        int maxScheduledRoutes = int.Min(
            VehicleRTIDocSeeder.RouteIds.Count,
            RouteStopDocSeeder.TemplateRouteStops.Count);
        for (int i = 0; i < maxScheduledRoutes; i++)
        {
            var selectedRouteStops = RouteStopDocSeeder.TemplateRouteStops[i];
            var scheduledRoute = new ScheduledRouteDoc
            {
                Id = Guid.NewGuid(),
                RouteId = VehicleRTIDocSeeder.RouteIds.ElementAt(i),
                VehicleId = faker.Random.CollectionItem(VehicleRTIDocSeeder.VehicleIds),
                StartTime = selectedRouteStops.First().DepartureTime,
                EndTime = selectedRouteStops.Last().DepartureTime,
                Stops = selectedRouteStops,
            };
            ScheduledRoutes.Add(scheduledRoute);
        }
        await scheduledRouteRepo.Upsert(ScheduledRoutes);
    }
}
