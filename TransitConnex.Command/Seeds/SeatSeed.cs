using MongoDB.Driver.Linq;
using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class SeatSeed
{
    public static void Seed(AppDbContext context)
    {
        var trains = context.Vehicles.Where(x => x.VehicleType == VehicleTypeEnum.TRAIN);
        var seatsToBeSeeded = new List<Seat>();
        var seatsPerVagon = 50;

        foreach (var train in trains)
        {
            var vagonId = 1;
            for (var i = 1; i <= train.Capacity; i++)
            {
                if (i % seatsPerVagon == 0)
                {
                    vagonId++;
                }

                var newSeat = new Seat() {VehicleId = train.Id, SeatNumber = i, VagonNumber = vagonId,};
                seatsToBeSeeded.Add(newSeat);
            }
        }

        foreach (var seat in seatsToBeSeeded)
        {
            context.Seats.Add(seat);
        }
        
        context.SaveChanges();
    }
}
