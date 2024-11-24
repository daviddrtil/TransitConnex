using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Command.Commands.Line;

public class LineUpdateCommand : ILineCommand
{
    public required Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public required string Label { get; set; }
    public required LineTypeEnum LineType { get; set; }
}
