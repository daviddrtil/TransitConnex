namespace TransitConnex.Command.Commands.Stop;

public class StopBatchCreateCommand : IStopCommand
{
    public required List<StopCreateCommand> Stops { get; set; }
}
