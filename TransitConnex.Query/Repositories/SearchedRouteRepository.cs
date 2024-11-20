using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

internal class SearchedRouteRepository(IReadDbContext readDbContext)
    : BaseRepository<SearchedRouteDoc, Guid>(readDbContext), ISearchedRouteRepository
{
}
