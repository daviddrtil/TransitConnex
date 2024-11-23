using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IVehicleRTIMongoRepository : IBaseMongoRepository<VehicleRTIDoc, Guid>
{
    Task<VehicleRTIDoc?> GetByVehicleId(Guid vehicleId);
}
