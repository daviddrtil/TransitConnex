namespace TransitConnex.Domain.DTOs.SearchedRoute;

public class SearchedRouteDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public DateTime Time { get; set; }
    public required ICollection<Guid> ScheduledRouteIds { get; set; }
}
