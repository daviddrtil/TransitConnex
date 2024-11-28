using System.Windows.Input;

namespace TransitConnex.Command.Commands.Line;

public class LineBatchUpdateCommand : ILineCommand
{
    public required List<LineUpdateCommand> Lines { get; set; }
}
