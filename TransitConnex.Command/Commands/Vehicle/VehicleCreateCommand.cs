using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Command.Commands.Vehicle;

public class VehicleCreateCommand : IVehicleCommand
{
    [MaxLength(255)]
    public required string Label { get; set; }
    [MaxLength(255)]
    public required string Spz { get; set; }
    [MaxLength(255)]
    public required string Manufacturer { get; set; }
    public required int Capacity { get; set; }
    public required VehicleTypeEnum VehicleType { get; set; }
    public Guid? IconId { get; set; }
    public Guid? LineId { get; set; }
}
