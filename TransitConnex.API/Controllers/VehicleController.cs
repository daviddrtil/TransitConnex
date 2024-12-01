using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.DTOs.Vehicle;

namespace TransitConnex.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VehicleController(
    VehicleCommandHandler vehicleCommandHandler,
    VehicleQueryHandler vehicleQueryHandler,
    VehicleRTIQueryHandler vehicleRTIQueryHandler) : Controller
{
    [HttpGet("GetRTIByVehicleId/{id}")]
    public async Task<VehicleRTIDto?> GetRTIByVehicleId(Guid id)
    {
        return await vehicleRTIQueryHandler.HandleGetByVehicleId(id);
    }

    [AuthorizedByAdmin]
    [HttpPost("AddVehicleRTI")]
    public async Task<Guid> AddVehicleRTI(VehicleRTIDto vehicleRTI)
    {
        return await vehicleRTIQueryHandler.HandleAddVehicleRTI(vehicleRTI);
    }

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

    /// <summary>
    /// Endpoint for creating new Vehicle.
    /// </summary>
    /// <param name="createCommand">Command containing all information about created vehicle.</param>
    /// <returns>Method status with id of created vehicle.</returns>
    [AuthorizedByAdmin]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVehicle(VehicleCreateCommand createCommand)
    {
        return Ok(await vehicleCommandHandler.HandleCreate(createCommand));
    }

    // /// <summary> // TODO -> not supported for first release
    // /// Ednpoint for creating multiple 
    // /// </summary>
    // /// <param name="createCommand"></param>
    // /// <returns></returns>
    // [AuthorizedByAdmin]
    // [HttpPost("batch")]
    // public async Task<ActionResult<List<Guid>>> CreateVehicles(VehicleBatchCreateCommand createCommand)
    // {
    //     return Ok(await vehicleCommandHandler.HandleBatchCreate(createCommand));
    // }

    /// <summary>
    /// Endpoint for updating Vehicle.
    ///
    /// Does not add seats -> for adding seats use SeatController.
    /// </summary>
    /// <param name="editCommand">Command containing updated information about vehicle.</param>
    /// <returns>Method status.</returns>
    [AuthorizedByAdmin]
    [HttpPut]
    public async Task<IActionResult> EditVehicle(VehicleUpdateCommand editCommand)
    {
        await vehicleCommandHandler.HandleUpdate(editCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting Vehicle from the system.
    ///
    /// Vehicle can only be deleted when it is not assigned to any ScheduledRoutes. -> use replace first
    /// </summary>
    /// <param name="id">Id of deleted vehicle</param>
    /// <returns>Method status.</returns>
    [AuthorizedByAdmin]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        await vehicleCommandHandler.HandleDelete(id); 
        return Ok();
    }

    // /// <summary> // TODO -> not supported for first release
    // /// 
    // /// </summary>
    // /// <param name="deleteIds"></param>
    // /// <returns></returns>
    // [AuthorizedByAdmin]
    // [HttpDelete("batch")]
    // public async Task<IActionResult> DeleteVehicles(List<Guid> deleteIds)
    // {
    //     // await VehicleCommandHandler.HandleDelete(deleteCommand); 
    //
    //     return Ok();
    // }

    /// <summary>
    /// Endpoint for replacing one vehicle on all scheduled routes with different vehicle.
    ///
    /// Works only if new vehicle has more or same number of seats.
    /// </summary>
    /// <param name="replaceCommand">Command containing vehicle ids.</param>
    /// <returns>Method status.</returns>
    [HttpPost("replace")]
    [AuthorizedByAdmin]
    public async Task<IActionResult> ReplaceVehicleOnScheduledRoutes(VehicleReplaceOnScheduledCommand replaceCommand)
    {
        await vehicleCommandHandler.HandleReplace(replaceCommand);
        return Ok();
    }
}
