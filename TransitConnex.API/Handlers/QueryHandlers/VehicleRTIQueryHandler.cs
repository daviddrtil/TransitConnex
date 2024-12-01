using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class VehicleRTIQueryHandler(
    IVehicleRTIMongoService vehicleRTIService) : IBaseQueryHandler<VehicleRTIDto>
{
    public async Task<VehicleRTIDto?> HandleGetByVehicleId(Guid id)
    {
        return await vehicleRTIService.GetByVehicleId(id);
    }

    public async Task<IEnumerable<VehicleRTIDto>> HandleGetAll()
    {
        return await vehicleRTIService.GetAll();
    }

    public async Task<Guid> HandleAddVehicleRTI(VehicleRTIDto vehicleRTI)
    {
        return await vehicleRTIService.Create(vehicleRTI);
    }
}
