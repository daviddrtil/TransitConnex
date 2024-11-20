using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IVehicleRTIRepository : IBaseRepository<VehicleRTIDoc, Guid>
{
    Task<VehicleRTIDoc> GetByVehicleIdAsync(Guid id);
}
