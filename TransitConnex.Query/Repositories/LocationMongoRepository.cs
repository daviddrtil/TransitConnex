using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class LocationMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<LocationDoc, Guid>(readDbContext), ILocationMongoRepository
{
    public async Task<IEnumerable<LocationDoc>> GetByName(string name)
    {
        var filter = Builders<LocationDoc>.Filter.Or(
            Builders<LocationDoc>.Filter.Eq(x => x.Name, name), // Exact match
            Builders<LocationDoc>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{name}", "i")) // Starts with (case-insensitive)
        );
        return await Collection
            .Find(filter)
            .Limit(10)
            .ToListAsync();
    }

    public async Task<LocationDoc> GetClosest(double latitude, double longitude)
    {
        var userCurrentLocation = new GeoJsonPoint<GeoJson2DCoordinates>(
            new GeoJson2DCoordinates(longitude, latitude));
        var filter = Builders<LocationDoc>.Filter.Near(l => l.Coordinates, userCurrentLocation);
        return await Collection
            .Find(filter)
            .FirstOrDefaultAsync();
    }
}
