using TransitConnex.Domain.DTOs.RouteStop;

namespace TransitConnex.Domain.DTOs.ScheduledRoute;

public class ScheduledRouteDto
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime StartTime { get; set; }
    public bool HasTickets { get; set; }
    public required ICollection<RouteStopDto> Stops { get; set; }
}
