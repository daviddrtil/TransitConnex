namespace TransitConnex.Infrastructure.Commands.Stop;

public class StopDeleteCommand : IStopCommand
{
    public required Guid Id { get; set; }
}
