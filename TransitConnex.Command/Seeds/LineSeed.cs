using TransitConnex.Command.Data;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class LineSeed
{
    public static void Seed(AppDbContext context)
    {
        var linesToBeSeeded = new List<Line>
        {
            new()
            {
                Id = Guid.Parse("aaa86126-8df7-4ee7-841f-02955ed3ab80"),
                Label = "R8", 
                Name = "Brno-Přerov", 
                LineType = LineTypeEnum.BUS
            },
            // new() {Label = "", Name = "", LineType = LineTypeEnum.BUS}
        };

        foreach (var line in linesToBeSeeded)
        {
            context.Lines.Add(line);
        }
        
        context.SaveChanges();
    }
}
