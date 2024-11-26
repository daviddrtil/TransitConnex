using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class StopSeed
{
    public static void Seed(AppDbContext context)
    {
        var stopsToBeSeeded = new List<Stop>
        {
            new()
            {
                Name = "Srbská", // start 32
                Latitude = 49.22820794257721,
                Longitude = 16.586961178108076,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Srbská", // end 32
                Latitude = 49.22820074109063,
                Longitude = 16.587075921603038,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Hutařova",
                Latitude = 49.22634058522127,
                Longitude = 16.588181334854493,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Hutařova",
                Latitude = 49.2260883430936,
                Longitude = 16.58855416190523,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Slovanské náměstí",
                Latitude = 49.223562092545166,
                Longitude = 16.590999878481067,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Slovanské náměstí",
                Latitude = 49.22278955352384,
                Longitude = 16.59131906135184,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Slovanské náměstí",
                Latitude = 49.22265641632737,
                Longitude = 16.591876960823445,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Charvatská",
                Latitude = 49.219588727488066,
                Longitude = 16.592263122779958,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Charvatská",
                Latitude = 49.21926987768618,
                Longitude = 16.592542072517343,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Šelepova",
                Latitude = 49.212901501007856,
                Longitude = 16.59627171934292,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Šelepova",
                Latitude = 49.213455178368626,
                Longitude = 16.596059824832068,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Botanická",
                Latitude = 49.21082908508684,
                Longitude = 16.597526906503102,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Botanická",
                Latitude = 49.210629330826556,
                Longitude = 16.597695885669985,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Zahradníkova",
                Latitude = 49.208187972510366,
                Longitude = 16.599140559180317,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Zahradníkova",
                Latitude = 49.207809469850574,
                Longitude = 16.599033270820392,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Sušilova",
                Latitude = 49.204433978784344,
                Longitude = 16.598647957118473,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Sušilova",
                Latitude = 49.204355118315505,
                Longitude = 16.598967139989245,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Smetanova",
                Latitude = 49.20135299654054,
                Longitude = 16.602555040868037,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Smetanova",
                Latitude = 49.20192783374987,
                Longitude = 16.602034692322402,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Česká", // End 32
                Latitude = 49.19898626808143,
                Longitude = 16.6034720262986,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Česká", // Start 32
                Latitude = 49.19834188851321,
                Longitude = 16.60516632740817,
                StopType = StopTypeEnum.BUS
            },
            new()
            {
                Name = "Česká",
                Latitude = 49.197670013023775,
                Longitude = 16.605743857327433,
                StopType = StopTypeEnum.TRAM
            },
            new()
            {
                Name = "Česká",
                Latitude = 49.19772434648215,
                Longitude = 16.606419773994954,
                StopType = StopTypeEnum.TRAM
            },
            new()
            {
                Name = "Česká",
                Latitude = 49.19742638807344,
                Longitude = 16.60740950911653,
                StopType = StopTypeEnum.TRAM
            },
            new()
            {
                Name = "Česká",
                Latitude = 49.197866314269035,
                Longitude = 16.607127877171724,
                StopType = StopTypeEnum.TRAM
            },
            new()
            {
                Name = "Přerov žst.",
                Latitude = 49.44776753919592, 
                Longitude = 17.44583561888692,
                StopType = StopTypeEnum.TRAIN 
            },
            new()
            {
                Name = "Brno hln.",
                Latitude = 49.19099098044296, 
                Longitude = 16.612740388729577,
                StopType = StopTypeEnum.TRAIN
            },
            new()
            {
                Name = "Vyškov, železniční Stanice",
                Latitude = 49.27819162530004, 
                Longitude = 16.99232046927684,
                StopType = StopTypeEnum.TRAIN
            },
        };

        foreach (var stop in stopsToBeSeeded)
        {
            context.Stops.Add(stop);
        }
        
        context.SaveChanges();
    }
}
