namespace TransitConnex.Command.Commands.RouteTicket;

public class RouteTicketDeleteCommand : IRouteTicketCommand
{
    public required List<Guid> Ids { get; set; }
}
