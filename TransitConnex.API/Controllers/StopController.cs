using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Stop;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StopController(StopCommandHandler stopCommandHandler) : Controller
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateStop(StopCreateCommand createCommand)
    {
        return await stopCommandHandler.HandleCreate(createCommand);
    }

    [HttpPost("batch")]
    public async Task<ActionResult<List<Guid>>> CreateStops(StopBatchCreateCommand createCommands)
    {
        return await stopCommandHandler.HandleBatchCreate(createCommands);
    }

    [HttpPut]
    public async Task<IActionResult> EditStop(StopUpdateCommand updateCommand)
    {
        await stopCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<StopUpdateCommand> updateCommand)
    {
        // TODO

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStop(Guid id)
    {
        await stopCommandHandler.HandleDelete(id); 

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteStops(List<Guid> deleteIds)
    {
        // await StopCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
