namespace TransitConnex.Domain.Models
{
    public class RouteTicket
    {
        public Guid Id { get; set; }
        public float Price { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid RouteId { get; set; }
        public ScheduledRoute? Route { get; set; }
        public Guid SeatId { get; set; }
        public Seat? Seat { get; set; }
    }
}
