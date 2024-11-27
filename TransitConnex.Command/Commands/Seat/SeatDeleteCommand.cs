namespace TransitConnex.Command.Commands.Seat;

public class SeatDeleteCommand : ISeatCommand
{
    public required Guid VehicleId { get; init; }
    public List<int> SeatNumbers { get; init; } = [];
    public int VagonNumber { get; init; }
}
