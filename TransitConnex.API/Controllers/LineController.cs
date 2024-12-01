using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Line;
using TransitConnex.Domain.DTOs.Line;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class LineController(LineCommandHandler lineCommandHandler, LineQueryHandler lineQueryHandler) : Controller
{
    [HttpPost("filter")]
    public async Task<ActionResult<List<LineDto>>> GetLinesFilteredSoT(LineFilteredQuery filter)
    {
        return Ok(await lineQueryHandler.HandleGetFiltered(filter));
    }
    
    /// <summary>
    /// Endpoint for creating new Line.
    /// </summary>
    /// <param name="createCommand">Command containing information about created line.</param>
    /// <returns>Method status with Id of created line.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLine(LineCreateCommand createCommand)
    {
        return Ok(await lineCommandHandler.HandleCreate(createCommand));
    }

    /// <summary>
    /// Endpoint for creating multiple new Lines.
    /// </summary>
    /// <param name="createCommand">Command containing list of create commands.</param>
    /// <returns>Method status with list of Ids of created lines.</returns>
    [HttpPost("batch")]
    public async Task<ActionResult<List<Guid>>> CreateLines(LineBatchCreateCommand createCommand)
    {
        return Ok(await lineCommandHandler.HandleBatchCreate(createCommand));
    }

    /// <summary>
    /// Endpoint for editing information about Line.
    /// </summary>
    /// <param name="updateCommand">Command containing updated information about line.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    public async Task<IActionResult> EditLine(LineUpdateCommand updateCommand)
    {
        await lineCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for editing information about multiple lines.
    /// </summary>
    /// <param name="updateCommand">Command containing list of update commands.</param>
    /// <returns>Method status.</returns>
    [HttpPut("batch")]
    public async Task<IActionResult> EditLines(LineBatchUpdateCommand updateCommand)
    {
        await lineCommandHandler.HandleBatchUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting Line from system.
    ///
    /// SERIOUS ACTION
    /// Will delete all linked Routes, ScheduledRouted, RouteTickets and SeatReservations.
    /// </summary>
    /// <param name="id">Id of deleted Line.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLine(Guid id)
    {
        await lineCommandHandler.HandleDelete(id);
        return Ok();
    }
}
