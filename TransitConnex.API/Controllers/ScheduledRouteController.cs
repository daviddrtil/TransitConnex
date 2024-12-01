using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ScheduledRouteController(
    UserManager<User> userManager,
    ScheduledRouteCommandHandler scheduledRouteCommandHandler,
    ScheduledRouteQueryHandler scheduledRouteQueryHandler) : Controller
{
    [HttpGet("GetScheduledRoutes")]
    public async Task<IEnumerable<ScheduledRouteDto>> GetScheduledRoutes(
        [Required] Guid startLocationId,
        [Required] Guid endLocationId,
        DateTime? startTime = null)
    {
        var user = await userManager.GetUserAsync(User);
        startTime = startTime is not null ?
            startTime.Value.ToUniversalTime()
            : DateTime.UtcNow; // Assign the current time if startTime is null
        var query = new ScheduledRouteGetAllQuery(user!.Id, startLocationId, endLocationId, startTime.Value);
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
    [AuthorizedByAdmin]
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
    [AuthorizedByAdmin]
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
    [AuthorizedByAdmin]
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
    [AuthorizedByAdmin]
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
    [AuthorizedByAdmin]
    public async Task<IActionResult> DeleteScheduledRoutes(List<Guid> deleteIds)
    {
        // await ScheduledRouteCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
