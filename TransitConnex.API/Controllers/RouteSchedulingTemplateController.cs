using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class RouteSchedulingTemplateController(
    RouteSchedulingTemplateCommandHandler routeSchedulingTemplateCommandHandler)
    : Controller
{
    /// <summary>
    /// Endpoint for getting all templates.
    /// </summary>
    /// <returns>Returns list of DTO with templates.</returns>
    [HttpGet]
    public async Task<ActionResult<List<RouteSchedulingTemplateDto>>> GetRouteSchedulingTemplates()
    {
        //TODO -> implement

        return Ok(null);
    }
    
    /// <summary>
    /// Endpoint for getting single Scheduling template.
    /// </summary>
    /// <param name="id">Id of template.</param>
    /// <returns>Returns DTO containing template. </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RouteSchedulingTemplateDto>> GetRouteSchedulingTemplates(string id)
    {
        //TODO -> implement

        return Ok(null);
    }
    
    /// <summary>
    /// Endpoint for creating Scheduling template.
    /// </summary>
    /// <param name="createCommand">Command containing template.</param>
    /// <returns>Method status.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRouteSchedulingTemplate(RouteSchedulingTemplateCreateCommand createCommand)
    {
        return await routeSchedulingTemplateCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for editing Scheduling template.
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
    /// Endpoint for deleting single Scheduling template.
    /// </summary>
    /// <param name="id">Id of Scheduling template.</param>
    /// <returns>Method status.</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteRouteSchedulingTemplate(Guid id)
    {
        await routeSchedulingTemplateCommandHandler.HandleDelete(id);  
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for deleting all Scheduling templates of given Route.
    /// </summary>
    /// <param name="routeId">Id of route for which we delete templates.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{routeId}")]
    public async Task<IActionResult> DeleteRouteSchedulingTemplatesForRoute(Guid routeId)
    {
        // await routeSchedulingTemplateCommandHandler.HandleDelete(routeId);  
        // TODO -> will delete all templates for given route

        return Ok();
    }
    
    /// <summary>
    /// Endpoint for running scheduler which will schedule routes using all templates of routes listed in command.
    /// </summary>
    /// <param name="runCommand">Command containing ids of all routes we want to schedule.</param>
    /// <returns>Method status.</returns>
    [HttpPost("run-scheduler-routes")]
    public async Task<IActionResult> RunSchedulerForRoutes(RouteSchedulingTemplateRunSchedulerCommand runCommand)
    {
        await routeSchedulingTemplateCommandHandler.HandleScheduler(runCommand);
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for running scheduler which will schedule all routes for the next year.
    /// </summary>
    /// <returns>Method status.</returns>
    [HttpPost("run-scheduler")]
    public async Task<IActionResult> RunScheduler()
    {
        // await routeSchedulingTemplateCommandHandler.HandleScheduler(); // TODO -> implement batch running
        return BadRequest();
    }
}
