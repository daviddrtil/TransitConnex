namespace TransitConnex.Infrastructure.Commands.ScheduledRoute;

public class ScheduledRouteCreateCommand : IScheduledRouteCommand
{
    // TODO -> will be only admin action
    public required DateTime StartTime { get; set; }
    public required Guid VehicleId { get; set; }
    public required Guid RouteId { get; set; }
}
