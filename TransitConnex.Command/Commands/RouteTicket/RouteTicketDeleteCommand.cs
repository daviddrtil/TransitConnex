namespace TransitConnex.Command.Commands.RouteTicket;

public class RouteTicketDeleteCommand : IRouteTicketCommand
{
    public required Guid Id { get; set; }
}
