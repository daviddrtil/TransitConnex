using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Services.Interfaces;

public interface IVehicleRTIMongoService
{
    Task<IEnumerable<VehicleRTIDto>> GetAll();
    Task<VehicleRTIDto?> GetByVehicleId(Guid id);
    Task<Guid> Create(VehicleRTIDto vehicleRTI);
}
