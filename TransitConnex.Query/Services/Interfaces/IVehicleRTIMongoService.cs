using TransitConnex.Domain.DTOs.VehicleRTI;

namespace TransitConnex.Query.Services.Interfaces;

public interface IVehicleRTIMongoService
{
    // todo VehicleRTIDto rewrite to model?
    Task<IEnumerable<VehicleRTIDto>> GetAll();
    Task<VehicleRTIDto?> GetById(Guid id);
    Task<Guid> Create(VehicleRTIDto vehicleRTI);
}
