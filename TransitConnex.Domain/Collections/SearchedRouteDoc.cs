namespace TransitConnex.Domain.Collections;

public class SearchedRouteDoc : QueryModelBase<Guid>
{
    public Guid UserId { get; set; }
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public DateTime Time { get; set; }
    public required ICollection<Guid> ScheduledRouteIds { get; set; }
}
