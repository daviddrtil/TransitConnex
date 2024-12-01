namespace TransitConnex.Command.Commands.Route;

public class RouteStopRemoveCommand : IRouteCommand
{
    public required Guid StopId { get; set; }
    public required Guid RouteId { get; set; }
}
