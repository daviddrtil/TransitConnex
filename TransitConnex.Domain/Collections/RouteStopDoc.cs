namespace TransitConnex.Domain.Collections;

public class RouteStopDoc : QueryModelBase<Guid>
{
    public DateTime Start { get; set; }
    public int TimeDurationFromFirstStop { get; set; }
    public int Order { get; set; }
}
