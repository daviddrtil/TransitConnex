using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class ServiceFilteredQuery : IServiceFilteredQuery
{
    public string? Name { get; set; }
    public List<Guid>? Ids { get; set; }
}
