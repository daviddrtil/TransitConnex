namespace TransitConnex.Application.Commands.Vehicle
{
    public class VehicleUpdateCommand
    {
        public Guid Id { get; set; }
        
        public required string Label  { get; set; }
    }
}
