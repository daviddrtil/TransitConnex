using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class IconFilteredQuery : IIconFilteredQuery
{
    public string? Name { get; set; }
    public bool VehicleIcons { get; set; }
    public List<Guid>? Ids { get; set; }
}
