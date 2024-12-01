using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Domain.DTOs.Route;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class RouteController(RouteCommandHandler routeCommandHandler, RouteQueryHandler routeQueryHandler) : Controller
{
    [HttpPost("filter")]
    public async Task<ActionResult<List<RouteDto>>> GetRoutesFilteredSoT(RouteFilteredQuery filter)
    {
        return Ok(await routeQueryHandler.HandleGetFiltered(filter));
    }
    
    /// <summary>
    /// Endpoint for creating new Route.
    /// </summary>
    /// <param name="createCommand"></param>
    /// <returns>Method status together with id of created route.</returns>
    [HttpPost]
    public async Task<Guid> CreateRoute(RouteCreateCommand createCommand)
    {
        return await routeCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for editing Route information.
    /// </summary>
    /// <param name="updateCommand">Command containing updated information about route.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    public async Task<IActionResult> EditRoute(RouteUpdateCommand updateCommand)
    {
        await routeCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting route.
    ///
    /// SERIOUS ACTION
    /// Will delete route together with ScheduledRoutes, RouteTickets and Reservations.
    /// </summary>
    /// <param name="id">Id of deleted route.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute(Guid id)
    {
        await routeCommandHandler.HandleDelete(id);

        return Ok();
    }
    
    /// <summary>
    /// Endpoint for adding stop to route.
    /// </summary>
    /// <param name="routeStopAddCommand"></param>
    /// <returns>Method status.</returns>
    [HttpPost("add-stop")]
    public async Task<IActionResult> AddStopToRoute(RouteStopAddCommand routeStopAddCommand)
    {
        await routeCommandHandler.HandleAddStopToRoute(routeStopAddCommand);
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for removing stop from route.
    /// </summary>
    /// <param name="routeStopAddCommand"></param>
    /// <returns>Method status.</returns>
    [HttpDelete("remove-stop")]
    public async Task<IActionResult> RemoveStopFromRoute(RouteStopRemoveCommand routeStopAddCommand)
    {
        await routeCommandHandler.HandleRemoveStopFromRoute(routeStopAddCommand);
        return Ok();
    }
}
