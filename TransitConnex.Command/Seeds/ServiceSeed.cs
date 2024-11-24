using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class ServiceSeed
{
    public static void Seed(AppDbContext context)
    {
        var servicesToBeSeeded = new List<Service>
        {
            new() {Name = "Wifi", Description = "", Icon = null},
            new()
            {
                Name = "Buffet",
                Description = "Vehicle obtains buffet which offers food and drinks.",
                Icon = null
            },
            new()
            {
                Name = "Boarding platform",
                Description = "Vehicle obtains boarding platform for people with disabilities.",
                Icon = null
            },
            new() {Name = "WC", Description = "", Icon = null}
        };
    }
}
