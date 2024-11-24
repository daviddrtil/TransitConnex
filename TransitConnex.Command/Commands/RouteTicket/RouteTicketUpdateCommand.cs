namespace TransitConnex.Command.Commands.RouteTicket;

public class RouteTicketUpdateCommand : IRouteTicketCommand
{
    // TODO -> will be only admin action
    public required Guid Id { get; set; }
    public required double Price { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ScheduledRouteId { get; set; }
    public required Guid SeatId { get; set; }
}
