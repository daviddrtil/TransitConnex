namespace TransitConnex.Domain.Collections;

public class SearchedRouteDoc : QueryModelBase<Guid>
{
    public Guid UserId { get; set; }
    public Guid FromLocationId { get; set; }
    public Guid ToLocationId { get; set; }
    public DateTime SearchTime { get; set; }
    public required ICollection<Guid> ScheduledRouteIds { get; set; }
}
