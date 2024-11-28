namespace TransitConnex.Query.Queries;

public class RouteSchedulingTemplateFilteredQuery
{
    public string? Name { get; set; }
    public List<Guid>? Ids { get; set; }
    public Guid? RouteId { get; set; }
}
