namespace TransitConnex.Domain.Models;

public class ScheduledRoute
{
    public Guid Id { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Guid RouteId { get; set; }
    public Route? Route { get; set; }
}
