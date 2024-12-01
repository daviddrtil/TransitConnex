using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class RouteFilteredQuery : IRouteFilteredQuery
{
    public Guid? LineId { get; set; }
    public bool IsWeekendRoute { get; set; }
    public bool IsActive { get; set; }
    public bool IsHolidayRoute { get; set; }
    public string? Name { get; set; }
}
