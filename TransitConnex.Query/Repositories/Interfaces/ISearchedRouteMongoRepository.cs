using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface ISearchedRouteMongoRepository : IBaseMongoRepository<SearchedRouteDoc, Guid>
{
}
