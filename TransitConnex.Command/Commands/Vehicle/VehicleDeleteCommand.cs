namespace TransitConnex.Command.Commands.Vehicle;

public class VehicleDeleteCommand : IVehicleCommand
{
    public Guid Id { get; set; }
}
