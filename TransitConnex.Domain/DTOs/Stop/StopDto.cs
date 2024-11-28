using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.DTOs.Stop;

public class StopDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public StopTypeEnum StopType { get; set; }
}
