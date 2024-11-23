using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

public class VehicleRTIMongoRepository(IReadDbContext readDbContext)
    : BaseMongoRepository<VehicleRTIDoc, Guid>(readDbContext), IVehicleRTIMongoRepository
{
    public async Task<VehicleRTIDoc?> GetByVehicleId(Guid vehicleId)
    {
        return await Collection
            .Find(v => v.VehicleId == vehicleId)
            .SortByDescending(v => v.Updated)
            .FirstOrDefaultAsync();
    }
}
