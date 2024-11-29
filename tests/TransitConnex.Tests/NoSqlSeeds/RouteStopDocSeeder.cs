using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Data;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Seeds;

public class RouteStopDocSeeder(
    AppDbContext context,
    IRouteStopMongoService routeStopService)
{
    //public static readonly List<List<RouteStopDoc>> TemplateRouteStops = [];
    //public static List<RouteStopDoc> AllRouteStops = [];
    //public async Task Seed()
    //{
    //    for (int i = 0; i < VehicleRTIDocSeeder.StopIds.Count; i++)
    //    {
    //        var start = faker.Date.Future().ToUniversalTime();
    //        start = start.AddSeconds(-start.Second)
    //            .AddMilliseconds(-start.Millisecond)
    //            .AddMicroseconds(-start.Microsecond); // round
    //        int stopsPerRoute = faker.Random.Int(5, 20);
    //        var currentRouteStops = new List<RouteStopDoc>();
    //        TemplateRouteStops.Add(currentRouteStops);
    //        for (int j = 0; j < stopsPerRoute; j++)
    //        {
    //            int offsetMinutes = faker.Random.Int(3, 8);
    //            var routeStop = new RouteStopDoc
    //            {
    //                Id = VehicleRTIDocSeeder.StopIds.ElementAt(i),
    //                Name = faker.Address.City(),
    //                DepartureTime = start.AddMinutes(offsetMinutes),
    //                Order = j + 1
    //            };
    //            start = start.AddMinutes(offsetMinutes);
    //            currentRouteStops.Add(routeStop);
    //            i++;
    //            if (i >= VehicleRTIDocSeeder.StopIds.Count)
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    AllRouteStops = TemplateRouteStops.SelectMany(x => x).ToList();
    //    await routeStopRepo.Upsert(AllRouteStops);
    //}

    public async Task Seed()
    {
        //var routeStops = await context.RouteStops.ToListAsync();
        //await routeStopService.Create(routeStops);
    }
}
