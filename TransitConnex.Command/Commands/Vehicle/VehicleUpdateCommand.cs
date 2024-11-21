using TransitConnex.Domain.Enums;

namespace TransitConnex.Infrastructure.Commands.Vehicle
{
    public class VehicleUpdateCommand : IVehicleCommand
    {
        public Guid Id { get; set; }
        
        public required string Label  { get; set; }
        public required string Spz { get; set; }
        public required string Manufacturer { get; set; }
        public required int Capacity { get; set; }
        public required VehicleTypeEnum VehicleType { get; set; }
        public Guid? IconId { get; set; }
        public Guid? LineId { get; set; }
    }
}
