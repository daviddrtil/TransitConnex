using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class StopController(StopCommandHandler stopCommandHandler, StopQueryHandler stopQueryHandler) : Controller
{
    [HttpPost("filter")]
    public async Task<ActionResult<List<StopDto>>> GetFilteredStops(StopFilteredQuery filter)
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
    /// Endpoint for updating multiple stops.
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns>Method status.</returns>
    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<StopUpdateCommand> updateCommand) // TODO
    {
        // TODO

        return Ok();
    }
    
    // TODO -> mby endpoint for moving stops to different location?

    /// <summary>
    /// Endpoint for deleting stop.
    /// </summary>
    /// <param name="id">Id of deleted stop.</param>
    /// <returns>Method status.</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteStop(Guid id)
    {
        await stopCommandHandler.HandleDelete(id); 
        return Ok();
    }

    /// <summary>
    /// Endpoint for
    /// </summary>
    /// <param name="deleteIds"></param>
    /// <returns>Method status.</returns>
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteStops(List<Guid> deleteIds)
    {
        // await StopCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
