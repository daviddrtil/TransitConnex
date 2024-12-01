using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class StopController(StopCommandHandler stopCommandHandler, StopQueryHandler stopQueryHandler) : Controller
{
    /// <summary>
    /// Endpoint for retrieving Stops by given filter.
    /// </summary>
    /// <param name="filter">Query containing filter properties.</param>
    /// <returns>Method status with list of DTOs containing information about stops.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<List<StopDto>>> GetFilteredStopsSoT(StopFilteredQuery filter)
    {
        return Ok(await stopQueryHandler.HandleGetFiltered(filter));
    }
    
    /// <summary>
    /// Endpoint for creating new stop.
    /// </summary>
    /// <param name="createCommand">Command containing all needed information for creating stop.</param>
    /// <returns>Method status with Id of created stop.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateStop(StopCreateCommand createCommand)
    {
        return await stopCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for creating multiple stops, each assigned to multiple locations.
    /// </summary>
    /// <param name="createCommands">Command containing list of stop create commands.</param>
    /// <returns>Method status with list of Ids of created stops.</returns>
    [HttpPost("batch")]
    public async Task<ActionResult<List<Guid>>> CreateStops(StopBatchCreateCommand createCommands)
    {
        return await stopCommandHandler.HandleBatchCreate(createCommands);
    }

    /// <summary>
    /// Endpoint for updating stop.
    /// </summary>
    /// <param name="updateCommand">Command containing updated info about stop.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    public async Task<IActionResult> EditStop(StopUpdateCommand updateCommand)
    {
        await stopCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for deleting stop.
    /// </summary>
    /// <param name="id">Id of deleted stop.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStop(Guid id)
    {
        await stopCommandHandler.HandleDelete(id); 
        return Ok();
    }

    /// <summary>
    /// Endpoint for adding Stop to Location.
    /// </summary>
    /// <param name="stopLocationCommand">Command containing id of stop and location.</param>
    /// <returns>Method status.</returns>
    [HttpPost("add-to-location")]
    public async Task<IActionResult> AddStopToLocation(StopLocationCommand stopLocationCommand)
    {
        await stopCommandHandler.HandleAddStopToLocation(stopLocationCommand);
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for removing Stop from Location.
    /// </summary>
    /// <param name="stopLocationCommand">Command containing id of stop and location.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("remove-from-location")]
    public async Task<IActionResult> RemoveStopFromLocation(StopLocationCommand stopLocationCommand)
    {
        await stopCommandHandler.HandleRemoveStopFromLocation(stopLocationCommand);
        return Ok();
    }
}
