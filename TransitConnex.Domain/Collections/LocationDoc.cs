using MongoDB.Driver.GeoJsonObjectModel;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.Collections;

public class LocationDoc : QueryModelBase<Guid>
{
    public string? Name { get; set; }
    public LocationTypeEnum LocationType { get; set; }
    public required GeoJsonPoint<GeoJson2DCoordinates> Coordinates { get; set; }
    public required ICollection<Guid> Stops { get; set; }
}
