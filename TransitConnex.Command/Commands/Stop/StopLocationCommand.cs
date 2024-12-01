namespace TransitConnex.Command.Commands.Stop;

public class StopLocationCommand : IStopCommand
{
    public required Guid StopId { get; set; }
    public required Guid LocationId { get; set; }
}
