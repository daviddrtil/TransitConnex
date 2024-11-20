namespace TransitConnex.Application.Commands.Vehicle
{
    public class VehicleCreateCommand : IVehicleCommand
    {
        public required string Label  { get; set; }
        public required string Spz { get; set; }
        public required string Manufacturer { get; set; }
        public required int Capacity { get; set; }
        public required int VehicleType { get; set; }
        public Guid? IconId { get; set; }
        public Guid? LineId { get; set; }
    }
}
