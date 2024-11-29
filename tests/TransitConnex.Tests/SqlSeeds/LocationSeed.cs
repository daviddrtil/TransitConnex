using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class LocationSeed
{
    public static readonly Location BrnoCityLocation = new()
    {
        Id = Guid.Parse("85103362-cef3-4626-8e34-7485c26a19ca"),
        Name = "Brno",
        Latitude = 49.193515714701626,
        Longitude = 16.607140355950424,
        LocationType = LocationTypeEnum.CITY
    };

    public static readonly Location VyskovCityLocation = new()
    {
        Id = Guid.Parse("73bc015e-3203-4258-8491-876ffd7880f6"),
        Name = "Vyškov",
        Latitude = 49.279085583407884,
        Longitude = 16.9960758136908,
        LocationType = LocationTypeEnum.CITY
    };

    public static readonly Location PrerovCityLocation = new()
    {
        Id = Guid.Parse("e0163341-e388-488f-be81-72a1ed659669"),
        Name = "Přerov",
        Latitude = 49.456268100985724,
        Longitude = 17.449163561863553,
        LocationType = LocationTypeEnum.CITY
    };

    public static readonly List<Location> Locations = [
        BrnoCityLocation,
        new()
        {
            Id = Guid.Parse("c9a5f3bc-4ffe-464d-8645-f338b631798e"),
            Name = "Brno - Hlavní nádraží",
            Latitude = 49.191106673687834,
            Longitude = 16.61228441319054,
            LocationType = LocationTypeEnum.CITY_PART
        },
        VyskovCityLocation,
        new()
        {
            Id = Guid.Parse("511d4c22-3019-47da-a580-d7abf828fcd0"),
            Name = "Vyškov, železniční Stanice",
            Latitude = 49.27819162530004,
            Longitude = 16.99232046927684,
            LocationType = LocationTypeEnum.CITY_PART
        },
        PrerovCityLocation,
        new()
        {
            Id = Guid.Parse("2f05fadc-be71-4216-9462-8e341cd34b51"),
            Name = "Přerov vlakové nádraží",
            Latitude = 49.44716817387072,
            Longitude = 17.446975391001338,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("91d6fc5b-23c2-4b10-bada-9e526abc4e1a"),
            Name = "Srbská",
            Latitude = 49.22817576491717,
            Longitude = 16.587008026187284,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("54d2fbea-2a81-42aa-b1cd-401c9d6c46b6"),
            Name = "Hutařova",
            Latitude = 49.22618939707557,
            Longitude = 16.58838131719683,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("58cf8d88-3759-4ddf-9215-bdec3b1ba97c"),
            Name = "Slovanské náměstí",
            Latitude = 49.223180761141336,
            Longitude = 16.591360034985705,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("bcb94181-cc1d-4592-b5cb-a97f79b78bf5"),
            Name = "Charvatská",
            Latitude = 49.21940200779551,
            Longitude = 16.59238195661406,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("99a14b3e-6c31-499b-bbc8-0e4065fc6036"),
            Name = "Šelepova",
            Latitude = 49.213188954676994,
            Longitude = 16.596161805645266,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("8e1e1b50-b4cd-46e8-8c55-553aa8b5fcb2"),
            Name = "Botanická",
            Latitude = 49.210739630555466,
            Longitude = 16.597607785146565,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("f64aeb16-82f8-4fb5-a638-428e913fd694"),
            Name = "Zahradníkova",
            Latitude = 49.2083775680841,
            Longitude = 16.599007898249308,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("b7bf5642-4c66-418e-b077-c8b2d65d999f"),
            Name = "Sušilova",
            Latitude = 49.204388908408795,
            Longitude = 16.598704539767176,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("b8856bdd-005a-486a-af79-8fff6694c817"),
            Name = "Smetanova",
            Latitude = 49.20153241457726,
            Longitude = 16.602023278740347,
            LocationType = LocationTypeEnum.CITY_PART
        },
        new()
        {
            Id = Guid.Parse("b734692a-273b-46f8-a451-37b67476947e"),
            Name = "Česká",
            Latitude = 49.19781325660542,
            Longitude = 16.60614822191168,
            LocationType = LocationTypeEnum.CITY_PART
        }
    ];


    public static readonly UserLocationFavourite UserFavLocationBrno = new()
    {
        UserId = UserSeed.BasicUser.Id,
        LocationId = BrnoCityLocation.Id,
        AddTime = DateTime.Parse("2024-11-14"),
    };

    private static void SeedUserFavLocations(AppDbContext context)
    {
        context.UserLocationFavourites.Add(UserFavLocationBrno);
        context.SaveChanges();
    }

    public static readonly UserConnectionFavourite UserFavConnectionBrnoPrerov = new()
    {
        UserId = UserSeed.BasicUser.Id,
        FromLocationId = BrnoCityLocation.Id,
        ToLocationId = PrerovCityLocation.Id,
        AddTime = DateTime.Parse("2024-11-14"),
    };

    private static void SeedUserFavConnections(AppDbContext context)
    {
        context.UserConnectionFavourites.Add(UserFavConnectionBrnoPrerov);
        context.SaveChanges();
    }

    public static void Seed(AppDbContext context)
    {
        foreach (var location in Locations)
        {
            if (!context.Locations.Any(x => x.Name == location.Name))
            {
                context.Locations.Add(location);
            }
        }
        context.SaveChanges();

        SeedUserFavLocations(context);
        SeedUserFavConnections(context);
    }
}
