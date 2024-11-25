using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Domain.DTOs;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleRTIController(
    VehicleRTIQueryHandler vehicleRTIQueryHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<VehicleRTIDto>> GetAll()
    {
        return await vehicleRTIQueryHandler.HandleGetAll();
    }

    [HttpGet("GetByVehicleId/{id}")]
    public async Task<VehicleRTIDto?> GetByVehicleId(Guid id)
    {
        return await vehicleRTIQueryHandler.HandleGetByVehicleId(id);
    }

    [HttpPost]
    public async Task<Guid> AddVehicleRTI(VehicleRTIDto vehicleRTI)
    {
        return await vehicleRTIQueryHandler.HandleCreate(vehicleRTI);
    }
}
