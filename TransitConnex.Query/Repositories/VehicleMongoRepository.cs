using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class VehicleMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<VehicleDoc, Guid>(readDbContext), IVehicleMongoRepository
{
}
