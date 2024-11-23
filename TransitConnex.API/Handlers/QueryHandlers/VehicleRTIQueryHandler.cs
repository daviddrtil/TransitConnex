using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs.VehicleRTI;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class VehicleRTIQueryHandler(
    IVehicleRTIMongoService vehicleRTIService) : IBaseQueryHandler<VehicleRTIDto>
{
    public async Task<VehicleRTIDto?> HandleGetById(Guid id)
    {
        return await vehicleRTIService.GetById(id);
        //if (query is VehicleRTIGetByIdQuery queryGetById)
        //{
        //    await vehicleRTIService.GetById(queryGetById.Id);
        //}
    }

    public async Task<IEnumerable<VehicleRTIDto>> HandleGetAll()
    {
        return await vehicleRTIService.GetAll();
    }

    public async Task<Guid> HandleCreate(VehicleRTIDto vehicleRTI)
    {
        return await vehicleRTIService.Create(vehicleRTI);
    }
}
