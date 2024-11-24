namespace TransitConnex.Infrastructure.Commands.Route;

public class RouteCreateCommand : IRouteCommand
{
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
