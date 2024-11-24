using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.Models;

public class Line
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public LineTypeEnum LineType { get; set; } // 1 - "busLine", 2 - "tramLine", 3 - "trainLine"
}
