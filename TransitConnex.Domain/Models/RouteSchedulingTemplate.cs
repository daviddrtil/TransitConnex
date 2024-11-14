namespace TransitConnex.Domain.Models
{
    public class RouteSchedulingTemplate
    {
        public Guid Id { get; set; }
        public Guid RouteId { get; set; }
        public string? Template { get; set; }
        public Route? Route { get; set; }
    }
}
