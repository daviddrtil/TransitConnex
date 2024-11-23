using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class SearchedRouteMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<SearchedRouteDoc, Guid>(readDbContext), ISearchedRouteMongoRepository
{
}
