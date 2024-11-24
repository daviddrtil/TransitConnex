namespace TransitConnex.Domain.Models;

public class LocationStop
{
    public Guid LocationId { get; set; }
    public Location? Location { get; set; }
    public Guid StopId { get; set; }
    public Stop? Stop { get; set; }
}
