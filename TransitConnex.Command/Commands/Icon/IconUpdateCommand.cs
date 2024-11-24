using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Infrastructure.Commands.Icon;

public class IconUpdateCommand : IIconCommand
{
    public required Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public required string Svg { get; set; }
}
