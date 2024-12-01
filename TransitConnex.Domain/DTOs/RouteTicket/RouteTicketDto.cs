namespace TransitConnex.Domain.DTOs.RouteTicket;

public class RouteTicketDto
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public Guid UserId { get; set; }
    public Guid RouteId { get; set; }
    public List<int> SeatNumbers { get; set; } = new List<int>();
}
