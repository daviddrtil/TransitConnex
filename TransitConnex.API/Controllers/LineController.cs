using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Line;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class LineController(LineCommandHandler lineCommandHandler) : Controller
{
    /// <summary>
    /// Endpoint for creating new Line.
    /// </summary>
    /// <param name="createCommand">Command containing information about created line.</param>
    /// <returns>Method status with Id of created line.</returns>
    [HttpPost]
    public async Task<Guid> CreateLine(LineCreateCommand createCommand)
    {
        return await lineCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for
    /// </summary>
    /// <param name="createCommand"></param>
    /// <returns>Method status</returns>
    [HttpPost("batch")]
    public async Task<List<Guid>> CreateLines(LineBatchCreateCommand createCommand)
    {
        return await lineCommandHandler.HandleBatchCreate(createCommand);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> EditLine(LineUpdateCommand updateCommand)
    {
        await lineCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns></returns>
    [HttpPut("batch")]
    public async Task<IActionResult> EditLines(List<LineUpdateCommand> updateCommand)
    {
        // TODO -> is needed?

        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLine(Guid id) // TODO -> very complicated command -> if line is deleted many factors can happen -> either with deleting line -> delete all routes + templates + scheduled + refund tickets? or some kind of soft delete which will result in routes not being scheduled for this line anymore (so basically all scheduled will stay but no new will be generated?) -> mby make choice optional? -> when line deleted assign null to vehicles.
    {
        await lineCommandHandler.HandleDelete(id);
        return Ok();
    }

    /// <summary> // TODO -> mby allow just one line deletion as it is very complicated operation which should not happen very often?
    /// 
    /// </summary>
    /// <param name="deleteIds"></param>
    /// <returns></returns>
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteLines(List<Guid> deleteIds)
    {
        // await lineCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
