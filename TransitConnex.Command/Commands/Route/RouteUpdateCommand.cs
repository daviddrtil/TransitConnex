namespace TransitConnex.Command.Commands.Route;

public class RouteUpdateCommand : IRouteCommand
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Direction { get; set; }
    public required TimeSpan DurationTime { get; set; }
    public required Guid LineId { get; set; }
    public required Guid StartStopId { get; set; }
    public required Guid EndStopId { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsWeekendRoute { get; set; }
    public required bool IsHolidayRoute { get; set; }
    public required bool HasTickets { get; set; }
}
