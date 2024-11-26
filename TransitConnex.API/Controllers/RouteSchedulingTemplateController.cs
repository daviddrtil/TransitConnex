using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouteSchedulingTemplateController(
    RouteSchedulingTemplateCommandHandler routeSchedulingTemplateCommandHandler)
    : Controller // TODO -> mby move to RouteController ? -> endpoints createSchedule, editSchedule, deleteSchedule -> delete would have boolean to know if delete also ScheduledRoutes?
{
    [HttpPost]
    public async Task<Guid> CreateRouteSchedulingTemplate(RouteSchedulingTemplateCreateCommand createCommand)
    {
        return await routeSchedulingTemplateCommandHandler.HandleCreate(createCommand);
    }

    [HttpPut]
    public async Task<IActionResult> EditRouteSchedulingTemplate(RouteSchedulingTemplateUpdateCommand updateCommand)
    {
        await routeSchedulingTemplateCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRouteSchedulingTemplate(Guid id)
    {
        await routeSchedulingTemplateCommandHandler.HandleDelete(id);  

        return Ok();
    }
}
