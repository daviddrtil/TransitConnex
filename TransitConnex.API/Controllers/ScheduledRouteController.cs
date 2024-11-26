using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> DeleteScheduledRoute(Guid id)
    {
        await scheduledRouteCommandHandler.HandleDelete(id); 

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteScheduledRoutes(List<Guid> deleteIds)
    {
        // await ScheduledRouteCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
