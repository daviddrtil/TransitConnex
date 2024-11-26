namespace TransitConnex.Command.Commands.Seat;

public class SeatBatchCreateCommand : ISeatCommand
{
    public required List<SeatCreateCommand> Seats { get; set; }
}
