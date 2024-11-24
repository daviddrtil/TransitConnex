namespace TransitConnex.Domain.Models;

public class Seat
{
    public Guid Id { get; set; }
    public int SeatNumber { get; set; }
    public int? VagonNumber { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
}
