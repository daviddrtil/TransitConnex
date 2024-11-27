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
                LineType = LineTypeEnum.TRAIN
            },
            new()
            {
                Id = Guid.Parse("84b25e26-d011-4487-86e2-53457b7c9e3f"),
                Label = "BRN - 32",
                Name = "Srbská-Česká",
                LineType = LineTypeEnum.BUS
            }
        };

        foreach (var line in linesToBeSeeded)
        {
            context.Lines.Add(line);
        }
        
        context.SaveChanges();
    }
}
