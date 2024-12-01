using TransitConnex.Domain.Enums;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class LineFilteredQuery : ILineFilteredQuery
{
    public LineTypeEnum? LineType { get; set; }
    public string? Name { get; set; }
}
