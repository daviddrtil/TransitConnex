using TransitConnex.Domain.Collections.NestedDocuments;

namespace TransitConnex.Query.Queries.VehicleRTI;

public class VehicleRTIGetByIdQuery : IVehicleRTIQuery
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime Updated { get; set; }
    public required Coordinate Coordinates { get; set; }
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
