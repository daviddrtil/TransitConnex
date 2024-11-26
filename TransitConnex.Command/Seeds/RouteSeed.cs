using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class RouteSeed
{
    public static void Seed(AppDbContext context)
    {
        var prerovTrainStop = context.Stops.FirstOrDefault(s => s.Name == "Přerov žst." && s.StopType == StopTypeEnum.TRAIN);
        var brnoTrainStop = context.Stops.FirstOrDefault(s => s.Name == "Brno hln." && s.StopType == StopTypeEnum.TRAIN);
        var linePrerovBrno = context.Lines.FirstOrDefault(l => l.Label == "R8");
        
        var routesToBeSeeded = new List<Route>
        {
            new()
            {
                Name = "R8 Brno-Přerov hlavní",
                Direction = "Přerov",
                DurationTime = new TimeSpan(1,10,0),
                HasTickets = true,
                StartStopId = brnoTrainStop!.Id,
                EndStopId = prerovTrainStop!.Id,
                IsHolydayRoute = false,
                IsWeekendRoute = false,
                IsActive = true,
                LineId = linePrerovBrno!.Id,
            },
            new()
            {
                Name = "R8 Brno-Přerov hlavní",
                Direction = "Brno",
                DurationTime = new TimeSpan(1,10,0),
                HasTickets = true,
                StartStopId = prerovTrainStop.Id,
                EndStopId = brnoTrainStop.Id,
                IsHolydayRoute = false,
                IsWeekendRoute = false,
                IsActive = true,
                LineId = linePrerovBrno.Id,
            }
        };

        foreach (var route in routesToBeSeeded)
        {
            context.Routes.Add(route);
        }
        
        context.SaveChanges();
    }
}
