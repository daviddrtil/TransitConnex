namespace TransitConnex.Command.Commands.Route;

public class RouteStopAddCommand : IRouteCommand
{
    public required Guid StopId { get; set; }
    public required Guid RouteId { get; set; }
    public required int StopOrder { get; set; }
    public required TimeSpan TimeDurationFromFirstStop { get; set; }
    public TimeSpan Delta { get; set; }
}
