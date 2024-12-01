using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System.Runtime.CompilerServices;
using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public class StopSeed
{
    public static readonly List<Stop> Stops = [
        new()
        {
            Id = Guid.Parse("bbdb98bb-e842-4f53-8f4b-f01fc66302e5"),
            Name = "Srbská", // start 32
            Latitude = 49.22820794257721,
            Longitude = 16.586961178108076,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("e3064840-6ff0-46a2-b9a4-c31ef7f3a640"),
            Name = "Srbská", // end 32
            Latitude = 49.22820074109063,
            Longitude = 16.587075921603038,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("8d72af3c-b043-48cd-840a-339706f23d37"),
            Name = "Hutařova",
            Latitude = 49.22634058522127,
            Longitude = 16.588181334854493,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("4f5e328a-4077-45f1-89ff-7200cf1c2cb3"),
            Name = "Hutařova",
            Latitude = 49.2260883430936,
            Longitude = 16.58855416190523,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("00e7292d-e082-42cf-afe6-c75c5ea8c0cd"),
            Name = "Slovanské náměstí",
            Latitude = 49.223562092545166,
            Longitude = 16.590999878481067,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("a700b35f-a985-451d-b695-d83b7321ec68"),
            Name = "Slovanské náměstí",
            Latitude = 49.22278955352384,
            Longitude = 16.59131906135184,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("a35b8ada-c70f-4306-a9ac-328c75e86d0a"),
            Name = "Slovanské náměstí",
            Latitude = 49.22265641632737,
            Longitude = 16.591876960823445,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("c44ceb9a-afe5-41fe-be89-b69e0c147e9f"),
            Name = "Charvatská",
            Latitude = 49.219588727488066,
            Longitude = 16.592263122779958,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("b817b1d4-95f6-49f9-919f-1a0c8712e8cb"),
            Name = "Charvatská",
            Latitude = 49.21926987768618,
            Longitude = 16.592542072517343,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("c6382781-66ff-47d5-abdf-c5784fa355aa"),
            Name = "Šelepova",
            Latitude = 49.212901501007856,
            Longitude = 16.59627171934292,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("d917770d-ed9d-4ef6-96f1-72dbe8e132aa"),
            Name = "Šelepova",
            Latitude = 49.213455178368626,
            Longitude = 16.596059824832068,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("30cdb774-1495-4819-b0b6-55d75c50c854"),
            Name = "Botanická",
            Latitude = 49.21082908508684,
            Longitude = 16.597526906503102,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("71d52553-1c4b-43ac-b7fc-0d3f89dc6468"),
            Name = "Botanická",
            Latitude = 49.210629330826556,
            Longitude = 16.597695885669985,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("23f8ad41-fbbb-4ad0-93f9-b05f582674b3"),
            Name = "Zahradníkova",
            Latitude = 49.208187972510366,
            Longitude = 16.599140559180317,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("f428b642-cce3-4509-a9c8-846f8e8db739"),
            Name = "Zahradníkova",
            Latitude = 49.207809469850574,
            Longitude = 16.599033270820392,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("c0d6a08f-c1d6-429e-b948-9e44f4756558"),
            Name = "Sušilova",
            Latitude = 49.204433978784344,
            Longitude = 16.598647957118473,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("00bd8fb5-2de4-4d34-9ef8-4766c75fc57d"),
            Name = "Sušilova",
            Latitude = 49.204355118315505,
            Longitude = 16.598967139989245,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("31598f96-4b3f-4166-9b94-3e8f5a7d1e9a"),
            Name = "Smetanova",
            Latitude = 49.20135299654054,
            Longitude = 16.602555040868037,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("c2624b0e-3af6-4a22-a7cd-9f36bc9bac49"),
            Name = "Smetanova",
            Latitude = 49.20192783374987,
            Longitude = 16.602034692322402,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("afff64d8-870a-4261-a471-bbda33d054f1"),
            Name = "Česká", // End 32
            Latitude = 49.19898626808143,
            Longitude = 16.6034720262986,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("de15e587-eddb-4607-97e2-0ce91494a8fa"),
            Name = "Česká", // Start 32
            Latitude = 49.19834188851321,
            Longitude = 16.60516632740817,
            StopType = StopTypeEnum.BUS
        },
        new()
        {
            Id = Guid.Parse("32318b89-ef7a-4485-b66a-45d379a52fe7"),
            Name = "Česká",
            Latitude = 49.197670013023775,
            Longitude = 16.605743857327433,
            StopType = StopTypeEnum.TRAM
        },
        new()
        {
            Id = Guid.Parse("d1b9889f-523f-49ec-bc8e-2f2b03979451"),
            Name = "Česká",
            Latitude = 49.19772434648215,
            Longitude = 16.606419773994954,
            StopType = StopTypeEnum.TRAM
        },
        new()
        {
            Id = Guid.Parse("93d0577c-f54b-4198-b1c1-40a2f76cab98"),
            Name = "Česká",
            Latitude = 49.19742638807344,
            Longitude = 16.60740950911653,
            StopType = StopTypeEnum.TRAM
        },
        new()
        {
            Id = Guid.Parse("94f3b2f4-2914-4aab-ba7c-dc4fb2c0a531"),
            Name = "Česká",
            Latitude = 49.19707900317785,
            Longitude = 16.608022717284363,
            StopType = StopTypeEnum.TRAM
        },
        new()
        {
            Id = Guid.Parse("010cf040-b380-4ec1-8104-81ae09dca489"),
            Name = "Česká",
            Latitude = 49.19630384981848,
            Longitude = 16.607913828622866,
            StopType = StopTypeEnum.TRAM
        },
        new()
        {
            Id = Guid.Parse("b743d2f6-13aa-493c-b1eb-d07e6d091e1d"),
            Name = "Přerov žst.",
            Latitude = 49.44776753919592,
            Longitude = 17.44583561888692,
            StopType = StopTypeEnum.TRAIN
        },
        new()
        {
            Id = Guid.Parse("02095ed5-8d2e-4d8d-9a4c-e1afef525305"),
            Name = "Brno hln.",
            Latitude = 49.19099098044296,
            Longitude = 16.612740388729577,
            StopType = StopTypeEnum.TRAIN
        },
        new()
        {
            Id = Guid.Parse("946e8d9b-8922-4b71-a5f0-850551e86d89"),
            Name = "Vyškov žst.",
            Latitude = 49.27819162530004,
            Longitude = 16.99232046927684,
            StopType = StopTypeEnum.TRAIN
        }
    ];

    public static void Seed(AppDbContext context)
    {
        foreach (var stop in Stops)
        {
            if (!context.Stops.Any(s => s.Id == stop.Id))
            {
                context.Stops.Add(stop);
            }
        }
        context.SaveChanges();

        var locations = context.Locations.Where(x => x.LocationType == LocationTypeEnum.CITY_PART)
            .AsNoTracking()
            .ToList();
        var locationStopsToBeSeeded = new List<LocationStop>();
        foreach (var location in locations) // seeding relations for city parts named as stops
        {
            var locationStops = Stops.Where(x => x.Name.Equals(location.Name)).ToList();
            locationStopsToBeSeeded.AddRange(locationStops.Select(locationStop => new LocationStop() { LocationId = location.Id, StopId = locationStop.Id, }));
        }

        locations = context.Locations.Where(x => x.LocationType == LocationTypeEnum.CITY)
            .AsNoTracking()
            .ToList();
        foreach (var location in locations) // seeding relations for cities
        {
            if (location.Name == "Brno")
            {
                var locationStops = Stops.Where(x => x.StopType != StopTypeEnum.TRAIN || x.Name == "Brno hln.").ToList();
                locationStopsToBeSeeded.AddRange(locationStops.Select(locationStop => new LocationStop() { LocationId = location.Id, StopId = locationStop.Id, }));
            }
            else
            {
                var locationStops = Stops.Where(x => x.StopType == StopTypeEnum.TRAIN && x.Name != "Brno hln." && x.Name.Contains(location.Name)).ToList();
                locationStopsToBeSeeded.AddRange(locationStops.Select(locationStop => new LocationStop() { LocationId = location.Id, StopId = locationStop.Id, }));
            }
        }

        // city parts with different name from stop
        locationStopsToBeSeeded.AddRange(new[]
        {
            new LocationStop()
            {
                StopId = Guid.Parse("946e8d9b-8922-4b71-a5f0-850551e86d89"),
                LocationId = Guid.Parse("511d4c22-3019-47da-a580-d7abf828fcd0")
            },
            new LocationStop()
            {
                StopId = Guid.Parse("b743d2f6-13aa-493c-b1eb-d07e6d091e1d"),
                LocationId = Guid.Parse("2f05fadc-be71-4216-9462-8e341cd34b51")
            },
            new LocationStop()
            {
                StopId = Guid.Parse("02095ed5-8d2e-4d8d-9a4c-e1afef525305"),
                LocationId = Guid.Parse("c9a5f3bc-4ffe-464d-8645-f338b631798e")
            }
        });

        foreach (var locationStop in locationStopsToBeSeeded)
        {
            if (!context.LocationStops.Any(x =>
                x.LocationId == locationStop.LocationId
                && x.StopId == locationStop.StopId))
            {
                context.LocationStops.Add(locationStop);
            }
        }
        context.SaveChanges();
    }
}
