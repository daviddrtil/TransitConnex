namespace TransitConnex.Command.Commands.Stop;

public class StopDeleteCommand : IStopCommand
{
    public required Guid Id { get; set; }
}
