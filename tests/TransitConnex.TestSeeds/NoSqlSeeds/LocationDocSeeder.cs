using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Data;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.TestSeeds.NoSqlSeeds;

public class LocationDocSeeder(
    AppDbContext context,
    ILocationMongoService locationService)
{
    //public static List<LocationDoc> Locations = [];
    //public async Task Seed()
    //{
    //    Locations = RouteStopDocSeeder.AllRouteStops.Select(
    //        stop => new LocationDoc
    //        {
    //            Id = Guid.NewGuid(),
    //            Name = stop.Name,
    //            LocationType = LocationTypeEnum.CITY,
    //            Coordinates = new GeoJsonPoint<GeoJson2DCoordinates>(
    //                new GeoJson2DCoordinates(
    //                    faker.Address.Latitude(49.0, 50.0),
    //                    faker.Address.Longitude(16.0, 17.0)
    //                )
    //            ),
    //            Stops = [stop.Id]
    //        }
    //    ).ToList();
    //    await locationRepo.Upsert(Locations);
    //}

    public async Task Seed()
    {
        var locations = await context.Locations
            .Include(l => l.Stops)
            .ToListAsync();
        var locationIds = await locationService.Create(locations);
    }
}
