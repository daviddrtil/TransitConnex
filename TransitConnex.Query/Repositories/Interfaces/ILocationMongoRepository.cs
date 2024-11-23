using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface ILocationMongoRepository : IBaseMongoRepository<LocationDoc, Guid>
{
}
