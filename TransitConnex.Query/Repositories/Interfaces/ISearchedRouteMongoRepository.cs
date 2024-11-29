using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface ISearchedRouteMongoRepository : IBaseMongoRepository<SearchedRouteDoc, Guid>
{
    Task<IEnumerable<SearchedRouteDoc>> GetByUserId(Guid userId);
}
