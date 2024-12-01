namespace TransitConnex.Command.Commands.ScheduledRoute;

public class ScheduledRouteCreateCommand : IScheduledRouteCommand
{
    public int Price { get; set; }
    public required DateTime StartTime { get; set; }
    public required Guid VehicleId { get; set; }
    public required Guid RouteId { get; set; }
}
