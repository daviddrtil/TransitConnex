using TransitConnex.Domain.Enums;

namespace TransitConnex.Query.Queries.Interfaces;

public class StopFilteredQuery : IStopFilteredQuery
{
    public List<Guid>? Ids { get; set; }
    public string? Name { get; set; }
    public StopTypeEnum StopType { get; set; }
    public Guid? LocationId { get; set; }
    public Guid? RouteId { get; set; }
}
