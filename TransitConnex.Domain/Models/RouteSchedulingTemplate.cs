namespace TransitConnex.Domain.Models;

public class RouteSchedulingTemplate
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Template { get; set; }
    public Guid RouteId { get; set; }
    public Route? Route { get; set; }
}
