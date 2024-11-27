using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.DTOs.Seat;

public class SeatDto
{
    public Guid Id { get; set; }
    public int SeatNumber { get; set; }
    public int VagonNumber { get; set; }
    public SeatStateEnum State { get; set; }
    public Guid VehicleId { get; set; }
}
