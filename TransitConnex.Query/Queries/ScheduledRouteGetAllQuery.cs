using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class ScheduledRouteGetAllQuery : IScheduledRouteQuery
{
    public Guid UserId { get; set; }
    public Guid StartLocationId { get; set; }
    public Guid EndLocationId { get; set; }
    public DateTime StartTime { get; set; }

    public ScheduledRouteGetAllQuery(Guid userId, 
        Guid startLocationId, Guid endLocationId, DateTime startTime)
    {
        UserId = userId;
        StartLocationId = startLocationId;
        EndLocationId = endLocationId;
        StartTime = startTime;
    }
}
