using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Domain.DTOs.Vehicle;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController(
    VehicleCommandHandler vehicleCommandHandler,
    VehicleQueryHandler vehicleQueryHandler) : Controller
{
    [HttpGet]
    public async Task<IEnumerable<VehicleDto>> GetVehicles()
    {
        return await vehicleQueryHandler.HandleGetAll();
    }

    [HttpGet("{id}")]
    public async Task<VehicleDto?> GetVehicle(Guid id)
    {
        return await vehicleQueryHandler.HandleGetById(id);
    }

    [AuthorizedByAdmin]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVehicle(VehicleCreateCommand createCommand)
    {
        return Ok(await vehicleCommandHandler.HandleCreate(createCommand));
    }

    [AuthorizedByAdmin]
    [HttpPost("batch")]
    public async Task<List<Guid>> CreateVehicles(VehicleBatchCreateCommand createCommand)
    {
        // return await VehicleCommandHandler.HandleCreate(createCommand);
        // return new Guid(); // TODO
        return new List<Guid>();
    }

    [AuthorizedByAdmin]
    [HttpPut]
    public async Task<IActionResult> UpdateVehicle(VehicleUpdateCommand editCommand)
    {
        await vehicleCommandHandler.HandleUpdate(editCommand);

        return Ok();
    }

    [AuthorizedByAdmin]
    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<VehicleUpdateCommand> updateCommand)
    {
        // TODO

        return Ok();
    }

    [AuthorizedByAdmin]
    [HttpDelete]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        await vehicleCommandHandler.HandleDelete(id); 
        return Ok();
    }

    [AuthorizedByAdmin]
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteVehicles(List<Guid> deleteIds)
    {
        // await VehicleCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
