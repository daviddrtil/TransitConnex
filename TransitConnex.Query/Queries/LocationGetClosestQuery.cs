using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Query.Queries;

public class LocationGetClosestQuery : ILocationQuery
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public LocationGetClosestQuery(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
