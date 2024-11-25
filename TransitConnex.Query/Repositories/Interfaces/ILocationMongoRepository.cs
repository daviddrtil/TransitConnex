using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface ILocationMongoRepository : IBaseMongoRepository<LocationDoc, Guid>
{
    Task<IEnumerable<LocationDoc>> GetByName(string name);
    Task<LocationDoc> GetClosest(double latitude, double longitude);
}
