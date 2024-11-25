using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class VehicleQueryHandler(
    IMapper mapper,
    IVehicleMongoService vehicleService) : IBaseQueryHandler<VehicleDto>
{
    public async Task<IEnumerable<VehicleDto>> HandleGetAll()
    {
        var vehicles = await vehicleService.GetAll();
        return mapper.Map<IEnumerable<VehicleDto>>(vehicles);
    }

    public async Task<VehicleDto?> HandleGetById(Guid id)
    {
        var vehicle = await vehicleService.GetById(id);
        if (vehicle == null)
            return null;
        return mapper.Map<VehicleDto>(vehicle);
    }
}
