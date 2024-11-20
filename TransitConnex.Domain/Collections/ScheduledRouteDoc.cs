namespace TransitConnex.Domain.Collections;

public class ScheduledRouteDoc : QueryModelBase<Guid>
{
    public Guid RouteId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public required ICollection<RouteStopDoc> Stops { get; set; }
}
