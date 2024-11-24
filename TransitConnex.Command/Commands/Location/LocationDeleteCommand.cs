namespace TransitConnex.Infrastructure.Commands.Location;

public class LocationDeleteCommand : ILocationCommand
{
    public required Guid Id { get; set; }
}
