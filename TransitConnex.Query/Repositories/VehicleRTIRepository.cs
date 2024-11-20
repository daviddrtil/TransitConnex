using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Repositories;

internal class VehicleRTIRepository(IReadDbContext readDbContext)
    : BaseRepository<VehicleRTIDoc, Guid>(readDbContext), IVehicleRTIRepository
{
    public async Task<VehicleRTIDoc> GetByVehicleIdAsync(Guid id)
    {
        return await Collection
            .Find(v => v.VehicleId == id)
            .SortByDescending(v => v.Updated)
            .FirstOrDefaultAsync();
    }
}
