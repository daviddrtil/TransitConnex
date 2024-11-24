namespace TransitConnex.Command.Commands.Seat;

public class SeatDeleteCommand : ISeatCommand
{
    public required Guid Id { get; set; }
}
