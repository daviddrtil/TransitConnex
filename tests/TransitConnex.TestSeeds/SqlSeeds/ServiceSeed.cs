using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public class ServiceSeed
{
    public static void Seed(AppDbContext context)
    {
        var servicesToBeSeeded = new List<Service>
        {
            new() {
                Id = Guid.Parse("3d6fdad5-289d-46af-9333-87c89bfe49de"),
                Name = "Wifi",
                Description = "",
                Icon = null
            },
            new()
            {
                Id = Guid.Parse("87d8d30c-3c92-4d2e-96f2-703f373ae448"),
                Name = "Buffet",
                Description = "Vehicle obtains buffet which offers food and drinks.",
                Icon = null
            },
            new()
            {
                Id = Guid.Parse("239c3456-1a7f-4922-a961-f770b3c5c551"),
                Name = "Boarding platform",
                Description = "Vehicle obtains boarding platform for people with disabilities.",
                Icon = null
            },
            new() {
                Id = Guid.Parse("8c015a66-aab3-489a-b143-b24dc7da2f0b"),
                Name = "WC",
                Description = "",
                Icon = null
            }
        };

        foreach (var service in servicesToBeSeeded)
        {
            context.Services.Add(service);
        }

        context.SaveChanges();
    }
}
