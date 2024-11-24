namespace TransitConnex.Command.Commands.ScheduledRoute;

public class ScheduledRouteDeleteCommand : IScheduledRouteCommand
{
    public required Guid Id { get; set; }
}
