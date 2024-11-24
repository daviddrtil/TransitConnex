namespace TransitConnex.Infrastructure.Commands.Route;

public class RouteDeleteCommand : IRouteCommand
{
    public required Guid Id { get; set; }
}
