using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Persistence;

namespace TransitConnex.Infrastructure.Seeds
{
    public class SeatSeed
    {
        public static void Seed(AppDbContext context)
        {
            var seatsToBeSeeded = new List<Seat>() { };
        }
    }
}
