namespace TransitConnex.Command.Commands.Route;

public class RouteUpdateCommand : IRouteCommand
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required TimeSpan DurationTime { get; set; }
    // TODO -> required?
    public Guid LineId { get; set; }
    public Guid StartStopId { get; set; }
    public Guid EndStopId { get; set; }
    public bool IsWeekendRoute { get; set; }
    public bool IsHolidayRoute { get; set; }
    public bool HasTickets { get; set; }
}
