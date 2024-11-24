using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class VehicleSeed
{
    public static void Seed(AppDbContext context)
    {
        var busIcon = context.Icons.FirstOrDefault(x => x.Name == "Bus");
        var trainIcon = context.Icons.FirstOrDefault(x => x.Name == "Train");
        var tramIcon = context.Icons.FirstOrDefault(x => x.Name == "Tram");

        var vehiclesToBeSeeded = new List<Vehicle>
        {
            new()
            {
                Capacity = 50,
                Icon = busIcon,
                Label = "Trol 32",
                Manufacturer = "Avia",
                VehicleType = VehicleTypeEnum.BUS,
                Spz = "6B5-2007"
            },
            new()
            {
                Capacity = 50,
                Icon = busIcon,
                Label = "Trol 32",
                Manufacturer = "Avia",
                VehicleType = VehicleTypeEnum.BUS,
                Spz = "6B5-2008"
            },
            new()
            {
                Capacity = 100,
                Icon = tramIcon,
                Label = "Tram 1",
                Manufacturer = "Škoda",
                VehicleType = VehicleTypeEnum.TRAM,
                Spz = "1911"
            },
            new()
            {
                Capacity = 100,
                Icon = tramIcon,
                Label = "Tram 1",
                Manufacturer = "Škoda",
                VehicleType = VehicleTypeEnum.TRAM,
                Spz = "1912"
            },
            new()
            {
                Capacity = 200,
                Icon = trainIcon,
                Label = "R8",
                Manufacturer = "RegioJet",
                VehicleType = VehicleTypeEnum.TRAIN,
                Spz = "114"
            },
            new()
            {
                Capacity = 200,
                Icon = trainIcon,
                Label = "R8",
                Manufacturer = "RegioJet",
                VehicleType = VehicleTypeEnum.TRAIN,
                Spz = "115"
            }
        };

        foreach (var vehicle in vehiclesToBeSeeded)
        {
            if (!context.Vehicles.Any(x =>
                    x.Label == vehicle.Label && x.VehicleType == vehicle.VehicleType && x.Spz == vehicle.Spz))
            {
                context.Vehicles.Add(vehicle);
            }
        }

        context.SaveChanges();

        foreach (var vehicle in vehiclesToBeSeeded.Where(x => x.VehicleType == VehicleTypeEnum.TRAIN))
        {
        }
    }
}
