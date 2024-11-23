namespace TransitConnex.Domain.DTOs.RouteStop;

public class RouteStopDto
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public int TimeDurationFromFirstStop { get; set; }
    public int Order { get; set; }
}
