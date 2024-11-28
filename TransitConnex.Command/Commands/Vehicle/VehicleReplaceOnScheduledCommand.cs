namespace TransitConnex.Command.Commands.Vehicle;

public class VehicleReplaceOnScheduledCommand : IVehicleCommand
{
    public Guid ReplacedId { get; set; }
    public Guid ReplacedById { get; set; }
}
