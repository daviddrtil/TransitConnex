using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class SeatFilteredQuery : ISeatFilteredQuery
{
    public Guid? VehicleId { get; set; }
}
