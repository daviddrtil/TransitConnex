using MongoDB.Driver.Linq;
using TransitConnex.Command.Data;
using TransitConnex.Command.Services;
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
                Id = Guid.Parse("1b23b003-4fd9-487e-9523-ed2ca383d66f"),
                Capacity = 50,
                Icon = busIcon,
                Label = "Trol 32",
                Manufacturer = "Avia",
                VehicleType = VehicleTypeEnum.BUS,
                Spz = "6B5-2007",
                IconId = busIcon!.Id,
                LineId = Guid.Parse("84b25e26-d011-4487-86e2-53457b7c9e3f"),
                Manufactured = DateTime.Parse("2021-09-01 00:00:00"),
                Vin = "1FAFP52UXXA197384"
            },
            new()
            {
                Id = Guid.Parse("caf27030-e49a-404c-ad1b-4bc4e15c094a"),
                Capacity = 50,
                Icon = busIcon,
                Label = "Trol 32",
                Manufacturer = "Avia",
                VehicleType = VehicleTypeEnum.BUS,
                Spz = "6B5-2008",
                IconId = busIcon!.Id,
                LineId = Guid.Parse("84b25e26-d011-4487-86e2-53457b7c9e3f"),
                Manufactured = DateTime.Parse("2022-09-01 00:00:00"),
                Vin = "3GNFK16318G269795"
            },
            new()
            {
                Id = Guid.Parse("b66925e9-0441-4b30-a2b2-86b0e8b2e348"),
                Capacity = 100,
                Icon = tramIcon,
                Label = "Tram 1",
                Manufacturer = "Škoda",
                VehicleType = VehicleTypeEnum.TRAM,
                Spz = "1911",
                IconId = tramIcon!.Id,
                Manufactured = DateTime.Parse("2021-09-01 00:00:00"),
                Vin = "5J6RE4H48BL023237"
            },
            new()
            {
                Id = Guid.Parse("47099fb6-d113-45c0-bd77-791a0a1d1a19"),
                Capacity = 100,
                Icon = tramIcon,
                Label = "Tram 1",
                Manufacturer = "Škoda",
                VehicleType = VehicleTypeEnum.TRAM,
                Spz = "1912",
                IconId = tramIcon!.Id,
                Manufactured = DateTime.Parse("2021-09-01 00:00:00"),
                Vin = "1HGCG2254WA015540"
            },
            new()
            {
                Id = Guid.Parse("687bdc2c-46c6-4dc0-b4fc-42bde1bd1006"),
                Capacity = 200,
                Icon = trainIcon,
                Label = "R8",
                Manufacturer = "RegioJet",
                VehicleType = VehicleTypeEnum.TRAIN,
                Spz = "114",
                IconId = trainIcon!.Id,
                LineId = Guid.Parse("aaa86126-8df7-4ee7-841f-02955ed3ab80"),
                Manufactured = DateTime.Parse("2021-09-01 00:00:00"),
                Vin = "JH4KA7660PC001313"
            },
            new()
            {
                Id = Guid.Parse("9d007ab8-dd0d-48bc-9756-ec5b3760ecb7"),
                Capacity = 200,
                Icon = trainIcon,
                Label = "R8",
                Manufacturer = "RegioJet",
                VehicleType = VehicleTypeEnum.TRAIN,
                Spz = "115",
                IconId = trainIcon!.Id,
                LineId = Guid.Parse("aaa86126-8df7-4ee7-841f-02955ed3ab80"),
                Manufactured = DateTime.Parse("2021-09-01 00:00:00"),
                Vin = "JNKCV54E46M706213"
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

        var vehicleServices = new List<VehicleOfferedService>();
        foreach (var vehicle in vehiclesToBeSeeded.Where(x => x.VehicleType == VehicleTypeEnum.TRAIN))
        {
            vehicleServices.AddRange(new[]
            {
                new VehicleOfferedService()
                {
                    VehicleId = vehicle.Id, ServiceId = Guid.Parse("3d6fdad5-289d-46af-9333-87c89bfe49de")
                },
                new VehicleOfferedService()
                {
                    VehicleId = vehicle.Id, ServiceId = Guid.Parse("87d8d30c-3c92-4d2e-96f2-703f373ae448")
                },
                new VehicleOfferedService()
                {
                    VehicleId = vehicle.Id, ServiceId = Guid.Parse("8c015a66-aab3-489a-b143-b24dc7da2f0b")
                }
            });
        }
        
        context.VehicleServices.AddRange(vehicleServices);
        context.SaveChanges();
    }
}
