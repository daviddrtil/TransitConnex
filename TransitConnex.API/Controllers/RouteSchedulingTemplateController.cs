using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class RouteSchedulingTemplateController(
    RouteSchedulingTemplateCommandHandler routeSchedulingTemplateCommandHandler,
    RouteSchedulingTemplateQueryHandler routeSchedulingTemplateQueryHandler
    ) : Controller
{
    /// <summary>
    /// Endpoint for getting all templates.
    /// </summary>
    /// <returns>Returns list of DTO with templates.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<List<RouteSchedulingTemplateDto>>> GetRouteSchedulingTemplatesSoT(RouteSchedulingTemplateFilteredQuery filter)
    {
        return Ok(await routeSchedulingTemplateQueryHandler.HandleGetFiltered(filter));
    }
    
    /// <summary>
    /// Endpoint for getting single SchedulingTemplate.
    /// </summary>
    /// <param name="id">Id of template.</param>
    /// <returns>Returns DTO containing template. </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RouteSchedulingTemplateDto>> GetRouteSchedulingTemplate(Guid id)
    {
        return Ok(await routeSchedulingTemplateQueryHandler.HandleGetById(id));
    }
    
    /// <summary>
    /// Endpoint for creating SchedulingTemplate.
    /// </summary>
    /// <param name="createCommand">Command containing template.</param>
    /// <returns>Method status.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRouteSchedulingTemplate(RouteSchedulingTemplateCreateCommand createCommand)
    {
        return await routeSchedulingTemplateCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for editing SchedulingTemplate.
    ///
    /// If UpdateExistingScheduledRoutes is set to true scheduler will be ran for this template only starting for scheduled routes of next day (tomorrows).
    /// </summary>
    /// <param name="updateCommand">Command containing new template.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    public async Task<IActionResult> EditRouteSchedulingTemplate(RouteSchedulingTemplateUpdateCommand updateCommand)
    {
        await routeSchedulingTemplateCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting single SchedulingTemplate.
    /// </summary>
    /// <param name="id">Id of Scheduling template.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRouteSchedulingTemplate(Guid id)
    {
        await routeSchedulingTemplateCommandHandler.HandleDelete(id);  
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for deleting all SchedulingTemplates of given Route.
    /// </summary>
    /// <param name="routeId">Id of route for which we delete templates.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("by-route/{routeId}")]
    public async Task<IActionResult> DeleteRouteSchedulingTemplatesForRoute(Guid routeId)
    {
        await routeSchedulingTemplateCommandHandler.HandleDeleteForRoute(routeId);  
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for running scheduler which will schedule routes using all templates of routes listed in command.
    ///
    /// SERIOUS ACTION.
    /// If reschedule is set to true, then all future scheduled routes will be deleted together with their tickets and reservations.
    /// </summary>
    /// <param name="runCommand">Command containing ids of all routes we want to schedule.</param>
    /// <returns>Method status.</returns>
    [HttpPost("run-scheduler")]
    public async Task<IActionResult> RunScheduler(RouteSchedulingTemplateRunSchedulerCommand runCommand)
    {
        await routeSchedulingTemplateCommandHandler.HandleScheduler(runCommand);
        return Ok();
    }
}
