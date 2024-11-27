using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Command.Commands.Location;

public class LocationCreateCommand : ILocationCommand
{
    [MaxLength(255)]
    public required string Name { get; set; }
    public required LocationTypeEnum Type { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
}
