using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class LocationSeed
{
    public static void Seed(AppDbContext context)
    {
        var locationsToBeSeeded = new List<Location>
        {
            new()
            {
                Name = "Brno",
                Latitude = 49.193515714701626,
                Longitude = 16.607140355950424,
                LocationType = LocationTypeEnum.CITY
            },
            new()
            {
                Name = "Srbská",
                Latitude = 49.22817576491717,
                Longitude = 16.587008026187284,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Hutařova",
                Latitude = 49.22618939707557,
                Longitude = 16.58838131719683,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Slovanské náměstí",
                Latitude = 49.223180761141336,
                Longitude = 16.591360034985705,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Charvatská",
                Latitude = 49.21940200779551,
                Longitude = 16.59238195661406,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Šelepova",
                Latitude = 49.213188954676994,
                Longitude = 16.596161805645266,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Botanická",
                Latitude = 49.210739630555466,
                Longitude = 16.597607785146565,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Zahradníkova",
                Latitude = 49.2083775680841,
                Longitude = 16.599007898249308,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Sušilova",
                Latitude = 49.204388908408795,
                Longitude = 16.598704539767176,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Smetanova",
                Latitude = 49.20153241457726,
                Longitude = 16.602023278740347,
                LocationType = LocationTypeEnum.CITY_PART
            },
            new()
            {
                Name = "Česká",
                Latitude = 49.19781325660542,
                Longitude = 16.60614822191168,
                LocationType = LocationTypeEnum.CITY_PART
            }
        };

        foreach (var location in locationsToBeSeeded)
        {
            if (!context.Locations.Any(x => x.Name == location.Name))
            {
                context.Locations.Add(location);
            }
        }

        context.SaveChanges();
    }
}
