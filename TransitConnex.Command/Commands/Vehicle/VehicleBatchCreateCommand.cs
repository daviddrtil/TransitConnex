namespace TransitConnex.Command.Commands.Vehicle;

public class VehicleBatchCreateCommand : IVehicleCommand
{
    public required List<VehicleCreateCommand> Vehicles { get; set; } 
}
