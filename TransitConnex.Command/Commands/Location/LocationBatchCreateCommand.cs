namespace TransitConnex.Command.Commands.Location;

public class LocationBatchCreateCommand : ILocationCommand
{
    public required List<LocationCreateCommand> Locations { get; set; } = new List<LocationCreateCommand>();
}
