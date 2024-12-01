namespace TransitConnex.Domain.DTOs.Route;

public class RouteDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan DurationTime { get; set; }
    public Guid LineId { get; set; }
    public Guid StartStopId { get; set; }
    public Guid EndStopId { get; set; }
    public bool IsActive { get; set; }
    public bool IsHolidayRoute { get; set; }
    public bool IsWeekendRoute { get; set; }
    public bool HasTickets { get; set; }
}
