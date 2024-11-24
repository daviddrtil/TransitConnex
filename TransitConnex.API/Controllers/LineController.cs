using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Infrastructure.Commands.Line;

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
    public async Task<Guid> CreateLines(List<LineCreateCommand> createCommands)
    {
        // return await lineCommandHandler.HandleCreate(createCommand);
        return new Guid(); // TODO
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
        // TODO

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLine(LineDeleteCommand deleteCommand)
    {
        await lineCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }
    
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteLines(List<Guid> deleteIds)
    {
        // await lineCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
