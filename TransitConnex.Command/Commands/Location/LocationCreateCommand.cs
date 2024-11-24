using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Infrastructure.Commands.Location;

public class LocationCreateCommand : ILocationCommand
{
    [MaxLength(255)]
    public required string Name { get; set; }
    public required LocationTypeEnum LocationType { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
}
