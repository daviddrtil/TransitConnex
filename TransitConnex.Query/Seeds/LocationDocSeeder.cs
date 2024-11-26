using Bogus;
using MongoDB.Driver.GeoJsonObjectModel;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Enums;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Seeds;

public class LocationDocSeeder(
    Faker faker,
    ILocationMongoRepository locationRepo)
{
    public static List<LocationDoc> Locations = [];

    public async Task Seed()
    {
        Locations = RouteStopDocSeeder.AllRouteStops.Select(
            stop => new LocationDoc
            {
                Id = Guid.NewGuid(),
                Name = stop.Name,
                LocationType = LocationTypeEnum.CITY,
                Coordinates = new GeoJsonPoint<GeoJson2DCoordinates>(
                    new GeoJson2DCoordinates(
                        faker.Address.Latitude(49.0, 50.0),
                        faker.Address.Longitude(16.0, 17.0)
                    )
                ),
                Stops = [stop.Id]
            }
        ).ToList();
        await locationRepo.Upsert(Locations);
    }
}
