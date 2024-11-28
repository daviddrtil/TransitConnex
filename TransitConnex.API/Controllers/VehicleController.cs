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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createCommand"></param>
    /// <returns></returns>
    [AuthorizedByAdmin]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVehicle(VehicleCreateCommand createCommand)
    {
        return Ok(await vehicleCommandHandler.HandleCreate(createCommand));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createCommand"></param>
    /// <returns></returns>
    [AuthorizedByAdmin]
    [HttpPost("batch")]
    public async Task<ActionResult<List<Guid>>> CreateVehicles(VehicleBatchCreateCommand createCommand)
    {
        return Ok(await vehicleCommandHandler.HandleBatchCreate(createCommand));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="editCommand"></param>
    /// <returns></returns>
    [AuthorizedByAdmin]
    [HttpPut]
    public async Task<IActionResult> EditVehicle(VehicleUpdateCommand editCommand)
    {
        await vehicleCommandHandler.HandleUpdate(editCommand);
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns></returns>
    [AuthorizedByAdmin]
    [HttpPut("batch")]
    public async Task<IActionResult> EditVehicles(List<VehicleUpdateCommand> updateCommand)
    {
        // TODO

        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AuthorizedByAdmin]
    [HttpDelete]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        await vehicleCommandHandler.HandleDelete(id); 
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deleteIds"></param>
    /// <returns></returns>
    [AuthorizedByAdmin]
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteVehicles(List<Guid> deleteIds)
    {
        // await VehicleCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }

    [HttpPost("replace")]
    public async Task ReplaceVehicleOnScheduledRoutes()
    {
        // TODO
    }
}
