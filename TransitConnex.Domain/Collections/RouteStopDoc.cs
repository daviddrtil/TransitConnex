namespace TransitConnex.Domain.Collections;

public class RouteStopDoc : QueryModelBase<Guid>
{
    public string? Name { get; set; }
    public DateTime DepartureTime { get; set; }
    public int Order { get; set; }
}
