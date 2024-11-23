using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class LocationMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<LocationDoc, Guid>(readDbContext), ILocationMongoRepository
{
}
