using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class LocationGetClosestQuery : ILocationQuery
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public LocationGetClosestQuery(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}
