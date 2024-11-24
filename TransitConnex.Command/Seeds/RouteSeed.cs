using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class RouteSeed
{
    public static void Seed(AppDbContext context)
    {
        var routesToBeSeeded = new List<Route> { new() { Name = "" } };
    }
}
