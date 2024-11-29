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

        context.AddRange(new List<Seat>()
        {
            new Seat()
            {
                Id = Guid.Parse("336ea3cb-dc62-46cf-bf19-a2512e5693a2"),
                SeatNumber = 1000,
                VagonNumber = 10,
                VehicleId = Guid.Parse("687bdc2c-46c6-4dc0-b4fc-42bde1bd1006")
            },
            new Seat()
            {
                Id = Guid.Parse("2adca51b-f093-48b1-b331-e63ff7490e01"),
                SeatNumber = 1000,
                VagonNumber = 10,
                VehicleId = Guid.Parse("9d007ab8-dd0d-48bc-9756-ec5b3760ecb7")
            }
        });
        
        context.SaveChanges();
    }
}
