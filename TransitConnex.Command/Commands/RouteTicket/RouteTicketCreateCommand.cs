using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Command.Commands.RouteTicket;

public class RouteTicketCreateCommand : IRouteTicketCommand
{
    [Range(0.00, 100000.00, ErrorMessage = "Price must be a positive number.")]
    public required float Price { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ScheduledRouteId { get; set; }
    public required List<Guid> SeatIds { get; set; }
}
