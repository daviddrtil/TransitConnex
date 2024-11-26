namespace TransitConnex.Command.Commands.Seat;

public class SeatUpdateCommand : ISeatCommand
{
    public required Guid Id { get; set; }
    public required int SeatNumber { get; set; }
    public int VagonNumber { get; set; }
}
