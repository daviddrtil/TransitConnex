namespace TransitConnex.Domain.Models;

public class VehicleOfferedService
{
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
}
