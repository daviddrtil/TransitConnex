using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class ScheduledRouteController(
    ScheduledRouteCommandHandler scheduledRouteCommandHandler,
    ScheduledRouteQueryHandler scheduledRouteQueryHandler) : Controller
{
    [HttpGet("GetScheduledRoutes")]
    public async Task<IEnumerable<ScheduledRouteDto>> GetScheduledRoutes(
        [Required] Guid startLocationId,
        [Required] Guid endLocationId,
        DateTime? startTime = null)
    {
        startTime ??= DateTime.Now; // Assign the current time if startTime is null
        var query = new ScheduledRouteGetAllQuery(startLocationId, endLocationId, startTime.Value);
        return await scheduledRouteQueryHandler.HandleGetScheduledRoutes(query);
    }

    /// <summary>
    /// Endpoint for creating ScheduledRoute.
    ///
    /// Only for special cases - for mass creating use scheduler instead.
    /// </summary>
    /// <param name="createCommand">Command containing information about created scheduled route.</param>
    /// <returns>Method status with Id of created scheduled route.</returns>
    [HttpPost]
    public async Task<Guid> CreateScheduledRoute(ScheduledRouteCreateCommand createCommand)
    {
        return await scheduledRouteCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> EditScheduledRoute(ScheduledRouteUpdateCommand updateCommand)
    {
        await scheduledRouteCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateCommand"></param>
    /// <returns></returns>
    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<ScheduledRouteUpdateCommand> updateCommand)
    {
        // await scheduledRouteCommandHandler.HandleUpdate(updateCommand); // TODO

        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteScheduledRoute(Guid id)
    {
        await scheduledRouteCommandHandler.HandleDelete(id); 

        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deleteIds"></param>
    /// <returns></returns>
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteScheduledRoutes(List<Guid> deleteIds)
    {
        // await ScheduledRouteCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
