using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Infrastructure.Commands.Vehicle;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController(VehicleCommandHandler vehicleCommandHandler) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
    {
        // return await _vehicleService.GetAllVehicles();
        return null;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto>> GetVehicle(Guid id)
    {
        // if (!await _vehicleService.VehicleExists(id))
        // {
        //     return NotFound($"Vehicle with id: {id} was not found.");
        // }
        //
        // return await _vehicleService.GetVehicleById(id);
        return null;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVehicle(VehicleCreateCommand createCommand)
    {
        return Ok(await vehicleCommandHandler.HandleCreate(createCommand));
    }
    
    [HttpPost("batch")]
    public async Task<Guid> CreateVehicles(List<VehicleCreateCommand> createCommands)
    {
        // return await VehicleCommandHandler.HandleCreate(createCommand);
        return new Guid(); // TODO
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVehicle(VehicleUpdateCommand editCommand)
    {
        await vehicleCommandHandler.HandleUpdate(editCommand);

        return Ok();
    }
    
    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<VehicleUpdateCommand> updateCommand)
    {
        // TODO

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteVehicle(VehicleDeleteCommand deleteCommand)
    {
        await vehicleCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }
    
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteVehicles(List<Guid> deleteIds)
    {
        // await VehicleCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
