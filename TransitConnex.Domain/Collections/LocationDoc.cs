using TransitConnex.Domain.Collections.NestedDocuments;

namespace TransitConnex.Domain.Collections;

public class LocationDoc : QueryModelBase<Guid>
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public required Coordinate Coordinates { get; set; }
}
