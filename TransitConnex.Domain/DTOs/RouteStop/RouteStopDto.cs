namespace TransitConnex.Domain.DTOs.RouteStop;

public class RouteStopDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime DepartureTime { get; set; }
    public int TimeDurationFromFirstStop { get; set; }
    public int Order { get; set; }
}
