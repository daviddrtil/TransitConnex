namespace TransitConnex.Command.Commands.Line;

public class LineBatchCreateCommand : ILineCommand
{
    public required List<LineCreateCommand> Lines { get; set; }
}
