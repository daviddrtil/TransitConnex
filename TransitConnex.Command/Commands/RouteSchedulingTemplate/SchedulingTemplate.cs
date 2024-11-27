namespace TransitConnex.Command.Commands.RouteSchedulingTemplate;

public class SchedulingTemplate
{
    public required List<ScheduledRoutes> ScheduledRoutes { get; set; }
}

public class ScheduledRoutes
{
    public required Guid VehicleId { get; set; }
    public required string StartTime { get; set; }
    public float Price { get; set; }
}
