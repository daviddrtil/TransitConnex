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
            new() {Label = "", Name = "", LineType = LineTypeEnum.BUS},
            new() {Label = "", Name = "", LineType = LineTypeEnum.BUS}
        };
    }
}
