using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Route;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouteController(RouteCommandHandler routeCommandHandler) : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="createCommand"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> EditRoute(RouteUpdateCommand updateCommand)
    {
        await routeCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteRoute(Guid id) // TODO -> very similar to deleting line -> delete scheduled or just mark as deleted and dont use for scheduling anymore?
    {
        await routeCommandHandler.HandleDelete(id);

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteRoutes(List<Guid> deleteIds)
    {
        // await RouteCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
