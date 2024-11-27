namespace TransitConnex.Command.Commands.Route;

public class RouteCreateCommand : IRouteCommand
{
    public required string Name { get; set; }
    public required TimeSpan DurationTime { get; set; }
    public required Guid LineId { get; set; }
    public required Guid StartStopId { get; set; }
    public required Guid EndStopId { get; set; }
    public required bool IsWeekendRoute { get; set; }
    public required bool IsHolidayRoute { get; set; }
    public required bool HasTickets { get; set; }
    public required bool MakeActive { get; set; }
    
    public required List<StopInRoute> Stops { get; set; }
}

public class StopInRoute
{
    public required Guid StopId { get; set; }
    public required int StopOrder { get; set; }
    public required TimeSpan TimeDurationFromFirstStop { get; set; }
}
