namespace TransitConnex.Command.Commands.Seat;

public class SeatCreateCommand : ISeatCommand
{
    public required int SeatNumber { get; set; }
    public int VagonNumber { get; set; }
    public required Guid VehicleId { get; set; }
}
