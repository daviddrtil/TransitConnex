using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.DTOs.Line;

public class LineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Label  { get; set; } = string.Empty;
    public LineTypeEnum LineType { get; set; }
}
