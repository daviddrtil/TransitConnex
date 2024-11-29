namespace TransitConnex.Domain.DTOs;

public class SearchedRouteDto
{
    public Guid Id { get; set; }// todo Id should here be UserId, and rewrite repository to add new ScheduledRouteIds
    public Guid UserId { get; set; }
    public Guid FromLocation { get; set; }
    public Guid ToLocation { get; set; }
    public DateTime Time { get; set; }
    public required ICollection<Guid> ScheduledRouteIds { get; set; }
}
