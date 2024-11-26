namespace TransitConnex.Domain.Models;

public class ScheduledRoute
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public float? Price { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Guid RouteId { get; set; }
    public Route? Route { get; set; }
}
