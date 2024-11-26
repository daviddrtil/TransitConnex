namespace TransitConnex.Domain.Models;

public class RouteTicket
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid ScheduledRouteId { get; set; }
    public ScheduledRoute? ScheduledRoute { get; set; }
}
