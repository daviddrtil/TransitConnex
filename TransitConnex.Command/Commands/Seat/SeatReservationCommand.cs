namespace TransitConnex.Command.Commands.Seat;

public class SeatReservationCommand : ISeatCommand
{
    public required Guid ScheduledRouteId { get; set; }
    public required List<Guid> SeatIds { get; set; }
    public required Guid UserId { get; set; }
}
