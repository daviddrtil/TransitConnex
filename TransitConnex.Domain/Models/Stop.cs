using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.Models;

public class Stop
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Label  { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public StopTypeEnum StopType { get; set; } // 1 - "busStop", 2 - "tramStop", 3 - "trainStop"
    
    public ICollection<LocationStop>? LocationStops { get; set; } = new List<LocationStop>();
    public ICollection<RouteStop>? RouteStops { get; set; } = new List<RouteStop>();
}
