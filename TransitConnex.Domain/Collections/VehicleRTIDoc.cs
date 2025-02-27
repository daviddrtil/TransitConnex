namespace TransitConnex.Domain.Collections;

public class VehicleRTIDoc : QueryModelBase<Guid>
{
    public Guid VehicleId { get; set; }
    public DateTime Updated { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double Speed { get; set; } // Speed in km/h
    public double Temperature { get; set; } // Temperature in celsius
    public int Delay { get; set; } // Delay in minutes
    public int Occupancy { get; set; }
    public bool IsInactive { get; set; }
    public bool IsStuck { get; set; }
    public Guid LineId { get; set; }
    public Guid ScheduledRouteId { get; set; }
    public Guid LastStopId { get; set; }
    public Guid FinalStopId { get; set; }
}
