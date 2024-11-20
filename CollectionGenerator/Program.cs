using Bogus;
using System.Text.Json;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Collections.NestedDocuments;

namespace CollectionGenerator;

class Program
{
    public static string ProjectPath { get; set; } = Path.Combine(Environment.CurrentDirectory, "..", "..", "..");
    public static string CollectionPath { get; set; } = Path.Combine(ProjectPath, "ParsedCollections");

    private static readonly Faker Faker = new("cz");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true, // For pretty-printing
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensures camelCase
    };

    private static void StoreAsJsonFile<T>(List<T> collection)
    {
        string path = Path.Combine(CollectionPath, $"{typeof(T).Name}.json");
        string jsonOutput = JsonSerializer.Serialize(collection, Options);
        File.WriteAllText(path, jsonOutput);
    }

    static void Main()
    {
        var vehicleRTIs = VehicleParser.LoadVehicleRTIs();
        StoreAsJsonFile(vehicleRTIs);

        var vehicleIds = new HashSet<Guid>();
        var routeIds = new HashSet<Guid>();
        var stopIds = new HashSet<Guid>();
        foreach (var v in vehicleRTIs)
        {
            vehicleIds.Add(v.VehicleId);
            routeIds.Add(v.ScheduledRouteId);
            stopIds.Add(v.LastStopId);
        }

        // Create locations
        string[] vehicleType = ["bus", "tram", "trolleybus"];
        var locations = stopIds.Select(
            stopId => new LocationDoc
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                Type = Faker.Random.CollectionItem(vehicleType),
                Coordinates = new Coordinate
                {
                    Latitude = Faker.Address.Latitude(49.0, 50.0),
                    Longitude = Faker.Address.Longitude(16.0, 17.0),
                },
            }
        ).ToList();
        StoreAsJsonFile(locations);

        // Create routeStops
        var routeStops = new List<RouteStopDoc>();
        for (int i = 0; i < stopIds.Count; i++)
        {
            DateTime start = Faker.Date.Future(1);
            int stopsPerRoute = Faker.Random.Int(5, 20);
            for (int j = 0; j < stopsPerRoute; j++)
            {
                var routeStop = new RouteStopDoc
                {
                    Id = stopIds.ElementAt(i),
                    Start = start,
                    TimeDurationFromFirstStop = Faker.Random.Int(20, 60),
                    Order = j + 1,
                };
                routeStops.Add(routeStop);
                i++;
                if (i >= stopIds.Count)
                    break;
            }
        }

        // Create scheduledRoutes
        var scheduledRoutes = routeIds.Select(
            routeId => new ScheduledRouteDoc
            {
                Id = Guid.NewGuid(),
                RouteId = routeId,
                VehicleId = Faker.Random.CollectionItem(vehicleIds),
                Start = Faker.Date.Past(1),
                End = Faker.Date.Future(1),
                Stops = Faker.Random.ListItems(routeStops, Faker.Random.Int(5, 20)),    // todo stops are not in order
            }
        ).ToList();
        StoreAsJsonFile(scheduledRoutes);

        // Create searchedRoutes
        var scheduledRouteIds = scheduledRoutes.Select(sr => sr.Id).ToList();
        var searchedRoutes = new List<SearchedRouteDoc>();
        for (int i = 0; i < 1000; i++)
        {
            var searchedRoute = new SearchedRouteDoc
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                FromLocation = Faker.Random.CollectionItem(locations).Name,
                ToLocation = Faker.Random.CollectionItem(locations).Name,
                Time = Faker.Date.Past(1),
                ScheduledRouteIds = Faker.Random.ListItems(scheduledRouteIds, 5),
            };
            searchedRoutes.Add(searchedRoute);
        }
        StoreAsJsonFile(searchedRoutes);
    }
}
