using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.ScheduledRoute;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduledRouteController(ScheduledRouteCommandHandler scheduledRouteCommandHandler) : Controller
{
    [HttpPost]
    public async Task<Guid> CreateScheduledRoute(ScheduledRouteCreateCommand createCommand)
    {
        return await scheduledRouteCommandHandler.HandleCreate(createCommand);
    }

    [HttpPost("batch")]
    public async Task<Guid> CreateScheduledRoutes(List<ScheduledRouteCreateCommand> createCommands)
    {
        // return await ScheduledRouteCommandHandler.HandleCreate(createCommand);
        return new Guid(); // TODO
    }

    [HttpPut]
    public async Task<IActionResult> EditScheduledRoute(ScheduledRouteUpdateCommand updateCommand)
    {
        await scheduledRouteCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<ScheduledRouteUpdateCommand> updateCommand)
    {
        // await scheduledRouteCommandHandler.HandleUpdate(updateCommand); // TODO

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteScheduledRoute(ScheduledRouteDeleteCommand deleteCommand)
    {
        await scheduledRouteCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteScheduledRoutes(List<Guid> deleteIds)
    {
        // await ScheduledRouteCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
