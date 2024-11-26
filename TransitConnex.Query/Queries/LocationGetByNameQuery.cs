using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class LocationGetByNameQuery : ILocationQuery
{
    public string Name { get; set; }

    public LocationGetByNameQuery(string name)
    {
        Name = name;
    }
}
