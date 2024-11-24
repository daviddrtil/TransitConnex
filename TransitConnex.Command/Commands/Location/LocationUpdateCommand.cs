using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Command.Commands.Location;

public class LocationUpdateCommand : ILocationCommand
{
    public required Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public required LocationTypeEnum LocationType { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
}
