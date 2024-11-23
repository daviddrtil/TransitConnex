using TransitConnex.Domain.DTOs.RouteStop;

namespace TransitConnex.Domain.DTOs.ScheduledRoute;

public class ScheduledRouteDto
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public required ICollection<RouteStopDto> Stops { get; set; }
}
