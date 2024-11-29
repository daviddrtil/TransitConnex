namespace TransitConnex.Domain.Collections;

public class SearchedRouteDoc : QueryModelBase<Guid>
{
    public Guid UserId { get; set; }
    public Guid FromLocation { get; set; }
    public Guid ToLocation { get; set; }
    public DateTime Time { get; set; }
    public required ICollection<Guid> ScheduledRouteIds { get; set; }
}
