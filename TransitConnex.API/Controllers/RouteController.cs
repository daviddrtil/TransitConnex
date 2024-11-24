using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Infrastructure.Commands.Route;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouteController(RouteCommandHandler routeCommandHandler) : Controller
{
    [HttpPost]
    public async Task<Guid> CreateRoute(RouteCreateCommand createCommand)
    {
        return await routeCommandHandler.HandleCreate(createCommand);
    }
    
    [HttpPost("batch")]
    public async Task<Guid> CreateRoutes(List<RouteCreateCommand> createCommands)
    {
        // return await RouteCommandHandler.HandleCreate(createCommand);
        return new Guid(); // TODO
    }

    [HttpPut]
    public async Task<IActionResult> EditRoute(RouteUpdateCommand updateCommand)
    {
        await routeCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoute(RouteDeleteCommand deleteCommand)
    {
        await routeCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }
    
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteRoutes(List<Guid> deleteIds)
    {
        // await RouteCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
