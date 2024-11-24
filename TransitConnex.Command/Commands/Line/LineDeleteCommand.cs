namespace TransitConnex.Command.Commands.Line;

public class LineDeleteCommand : ILineCommand
{
    public required Guid Id { get; init; }
}
