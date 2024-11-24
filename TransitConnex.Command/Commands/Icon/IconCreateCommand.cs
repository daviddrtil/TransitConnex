using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Command.Commands.Icon;

public class IconCreateCommand : IIconCommand
{
    [MaxLength(255)]
    public required string Name { get; set; }
    public required string Svg { get; set; }
}
