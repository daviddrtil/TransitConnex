using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public class VehicleSeed
    {
        public static void Seed(AppDbContext context)
        {
            var busIcon = context.Icons.FirstOrDefault(x => x.Name == "Bus");
            var trainIcon = context.Icons.FirstOrDefault(x => x.Name == "Train");
            var tramIcon = context.Icons.FirstOrDefault(x => x.Name == "Tram");

            var vehiclesToBeSeeded = new List<Vehicle>()
            {
                new()
                {
                    Capacity = 50,
                    Icon = busIcon,
                    Label = "Trol 32",
                    Manufacturer = "Avia",
                    VehicleType = 1,
                    Spz = "Neco-SPZ"
                },
                new()
                {
                    Capacity = 100,
                    Icon = busIcon,
                    Label = "Tram 1",
                    Manufacturer = "Škoda",
                    VehicleType = 2,
                    Spz = "Neco-SPZ"
                }
            };
        }
    }
}
