namespace TransitConnex.Domain.Collections;

public class ScheduledRouteDoc : QueryModelBase<Guid>
{
    public Guid RouteId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool HasTickets { get; set; }
    public required ICollection<RouteStopDoc> Stops { get; set; }
}
