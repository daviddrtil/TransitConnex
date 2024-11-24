using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Infrastructure.Commands.Line;

public class LineCreateCommand : ILineCommand
{
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public required string Label { get; set; }
    public required LineTypeEnum LineType { get; set; }
}
