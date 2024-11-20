using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

internal class LocationRepository(IReadDbContext readDbContext)
    : BaseRepository<LocationDoc, Guid>(readDbContext), ILocationRepository
{
}
