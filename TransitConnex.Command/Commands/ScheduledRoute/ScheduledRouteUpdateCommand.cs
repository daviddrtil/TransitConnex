namespace TransitConnex.Command.Commands.ScheduledRoute;

public class ScheduledRouteUpdateCommand : IScheduledRouteCommand
{
    public required Guid Id { get; set; }
    public required DateTime StartTime { get; set; }
    public required Guid VehicleId { get; set; }
}
