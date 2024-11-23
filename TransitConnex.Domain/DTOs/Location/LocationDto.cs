using TransitConnex.Domain.Collections.NestedDocuments;

namespace TransitConnex.Domain.DTOs.Location
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public required Coordinate Coordinates { get; set; }
    }
}
