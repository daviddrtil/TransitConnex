using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class ScheduledRouteGetAllQuery : IScheduledRouteQuery
{
    public Guid StartLocationId { get; set; }
    public Guid EndLocationId { get; set; }
    public DateTime StartTime { get; set; }

    public ScheduledRouteGetAllQuery(Guid startLocationId, Guid endLocationId, DateTime startTime)
    {
        StartLocationId = startLocationId;
        EndLocationId = endLocationId;
        StartTime = startTime;
    }
}
