using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Line;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LineController(LineCommandHandler lineCommandHandler) : Controller
{
    [HttpPost]
    public async Task<Guid> CreateLine(LineCreateCommand createCommand)
    {
        return await lineCommandHandler.HandleCreate(createCommand);
    }

    [HttpPost("batch")]
    public async Task<List<Guid>> CreateLines(LineBatchCreateCommand createCommand)
    {
        return await lineCommandHandler.HandleBatchCreate(createCommand);
    }

    [HttpPut]
    public async Task<IActionResult> EditLine(LineUpdateCommand updateCommand)
    {
        await lineCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<LineUpdateCommand> updateCommand)
    {
        // TODO -> is needed?

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLine(Guid id)
    {
        await lineCommandHandler.HandleDelete(id);

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteLines(List<Guid> deleteIds)
    {
        // await lineCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
