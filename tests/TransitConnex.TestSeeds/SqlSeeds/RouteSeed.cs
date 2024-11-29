using MongoDB.Driver.Linq;
using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public class RouteSeed
{
    private static readonly Stop PrerovTrainStop = StopSeed.Stops
        .First(s => s.Name == "Přerov žst." && s.StopType == StopTypeEnum.TRAIN);
    private static readonly Stop BrnoTrainStop = StopSeed.Stops
        .First(s => s.Name == "Brno hln." && s.StopType == StopTypeEnum.TRAIN);
    private static readonly Line LinePrerovBrno = LineSeed.Lines.First(l => l.Label == "R8");

    public static readonly List<Route> Routes = [
        new()
        {
            Id = Guid.Parse("9d8c5c9f-ca03-4399-96ff-8f081b67d298"),
            Name = "R8 Brno-Přerov hlavní",
            Direction = "Přerov",
            DurationTime = new TimeSpan(1,10,0),
            HasTickets = true,
            StartStopId = BrnoTrainStop!.Id,
            EndStopId = PrerovTrainStop!.Id,
            IsHolydayRoute = false,
            IsWeekendRoute = false,
            IsActive = true,
            LineId = LinePrerovBrno!.Id,
        },
        new()
        {
            Id = Guid.Parse("dff25738-54e3-4190-b19d-282a300c8219"),
            Name = "R8 Přerov-Brno hlavní",
            Direction = "Brno",
            DurationTime = new TimeSpan(1,10,0),
            HasTickets = true,
            StartStopId = PrerovTrainStop.Id,
            EndStopId = BrnoTrainStop.Id,
            IsHolydayRoute = false,
            IsWeekendRoute = false,
            IsActive = true,
            LineId = LinePrerovBrno.Id,
        },
        new()
        {
            Id = Guid.Parse("f83d0060-c5ad-4e68-ac4d-8e9f1e7364e2"),
            Name = "BRN-32 Srbská-Česká",
            Direction = "Česká",
            DurationTime = new TimeSpan(0,14,0),
            HasTickets = false,
            StartStopId = Guid.Parse("bbdb98bb-e842-4f53-8f4b-f01fc66302e5"),
            EndStopId = Guid.Parse("afff64d8-870a-4261-a471-bbda33d054f1"),
            IsHolydayRoute = false,
            IsWeekendRoute = false,
            IsActive = true,
            LineId = Guid.Parse("84b25e26-d011-4487-86e2-53457b7c9e3f"),
        },
        new()
        {
            Id = Guid.Parse("829c534e-ae42-48f9-b66f-3f5521c522c3"),
            Name = "BRN-32 Česká-Srbská",
            Direction = "Srbská",
            DurationTime = new TimeSpan(0,14,0),
            HasTickets = false,
            StartStopId = Guid.Parse("de15e587-eddb-4607-97e2-0ce91494a8fa"),
            EndStopId = Guid.Parse("e3064840-6ff0-46a2-b9a4-c31ef7f3a640"),
            IsHolydayRoute = false,
            IsWeekendRoute = false,
            IsActive = true,
            LineId = Guid.Parse("84b25e26-d011-4487-86e2-53457b7c9e3f"),
        }
    ];

    public static void Seed(AppDbContext context)
    {
        context.Routes.AddRange(Routes);
        context.SaveChanges();

        // TODO -> route_stops
        var busStopsIds = new List<Guid>
        {
            Guid.Parse("bbdb98bb-e842-4f53-8f4b-f01fc66302e5"),
            Guid.Parse("8d72af3c-b043-48cd-840a-339706f23d37"),
            Guid.Parse("00e7292d-e082-42cf-afe6-c75c5ea8c0cd"),
            Guid.Parse("c44ceb9a-afe5-41fe-be89-b69e0c147e9f"),
            Guid.Parse("c6382781-66ff-47d5-abdf-c5784fa355aa"),
            Guid.Parse("30cdb774-1495-4819-b0b6-55d75c50c854"),
            Guid.Parse("23f8ad41-fbbb-4ad0-93f9-b05f582674b3"),
            Guid.Parse("c0d6a08f-c1d6-429e-b948-9e44f4756558"),
            Guid.Parse("31598f96-4b3f-4166-9b94-3e8f5a7d1e9a"),
            Guid.Parse("afff64d8-870a-4261-a471-bbda33d054f1")
        };
        var busStops = context.Stops.Where(s => busStopsIds.Contains(s.Id)).ToList();
        int busStopDuration = 93;
        var routeStops = new List<RouteStop>();
        routeStops.AddRange(busStops.Select((stop, index) => new RouteStop
        {
            StopId = stop.Id,
            RouteId = Guid.Parse("f83d0060-c5ad-4e68-ac4d-8e9f1e7364e2"),
            StopOrder = index,
            TimeDurationFromFirstStop = new TimeSpan(0, 0, index * busStopDuration),
        }));

        busStopsIds = new List<Guid>
        {
            Guid.Parse("de15e587-eddb-4607-97e2-0ce91494a8fa"),
            Guid.Parse("c2624b0e-3af6-4a22-a7cd-9f36bc9bac49"),
            Guid.Parse("00bd8fb5-2de4-4d34-9ef8-4766c75fc57d"),
            Guid.Parse("f428b642-cce3-4509-a9c8-846f8e8db739"),
            Guid.Parse("71d52553-1c4b-43ac-b7fc-0d3f89dc6468"),
            Guid.Parse("d917770d-ed9d-4ef6-96f1-72dbe8e132aa"),
            Guid.Parse("b817b1d4-95f6-49f9-919f-1a0c8712e8cb"),
            Guid.Parse("a700b35f-a985-451d-b695-d83b7321ec68"),
            Guid.Parse("4f5e328a-4077-45f1-89ff-7200cf1c2cb3"),
            Guid.Parse("e3064840-6ff0-46a2-b9a4-c31ef7f3a640")
        };
        busStops = context.Stops.Where(s => busStopsIds.Contains(s.Id)).ToList();
        routeStops.AddRange(busStops.Select((stop, index) => new RouteStop
        {
            StopId = stop.Id,
            RouteId = Guid.Parse("829c534e-ae42-48f9-b66f-3f5521c522c3"),
            StopOrder = index,
            TimeDurationFromFirstStop = new TimeSpan(0, 0, index * busStopDuration),
        }));

        var trainStopsIds = new List<Guid>
        {
            Guid.Parse("b743d2f6-13aa-493c-b1eb-d07e6d091e1d"),
            Guid.Parse("946e8d9b-8922-4b71-a5f0-850551e86d89"),
            Guid.Parse("02095ed5-8d2e-4d8d-9a4c-e1afef525305")
        };
        var trainStops = context.Stops.Where(s => trainStopsIds.Contains(s.Id)).ToList();
        int trainStopDuration = 2100;
        routeStops.AddRange(trainStops.Select((stop, index) => new RouteStop
        {
            StopId = stop.Id,
            RouteId = Guid.Parse("dff25738-54e3-4190-b19d-282a300c8219"),
            StopOrder = index,
            TimeDurationFromFirstStop = new TimeSpan(0, 0, index * trainStopDuration),
        }));

        trainStopsIds = new List<Guid>
        {
            Guid.Parse("02095ed5-8d2e-4d8d-9a4c-e1afef525305"),
            Guid.Parse("946e8d9b-8922-4b71-a5f0-850551e86d89"),
            Guid.Parse("b743d2f6-13aa-493c-b1eb-d07e6d091e1d"),
        };
        trainStops = context.Stops.Where(s => trainStopsIds.Contains(s.Id)).ToList();
        routeStops.AddRange(trainStops.Select((stop, index) => new RouteStop
        {
            StopId = stop.Id,
            RouteId = Guid.Parse("9d8c5c9f-ca03-4399-96ff-8f081b67d298"),
            StopOrder = index,
            TimeDurationFromFirstStop = new TimeSpan(0, 0, index * trainStopDuration),
        }));

        context.RouteStops.AddRange(routeStops);
        context.SaveChanges();
    }
}
