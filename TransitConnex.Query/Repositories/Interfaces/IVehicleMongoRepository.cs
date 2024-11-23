using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IVehicleMongoRepository : IBaseMongoRepository<VehicleDoc, Guid>
{
}
