using TransitConnex.Domain.Enums;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class LocationFilteredQuery : ILocationFilteredQuery
{
    public LocationTypeEnum? LocationType { get; set; }
    public string? Name { get; set; }
}
