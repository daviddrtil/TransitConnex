using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class ScheduledRouteFilteredQuery : IScheduledRouteFilteredQuery
{
    public Guid? VehicleId { get; set; }
    public Guid? RouteId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
