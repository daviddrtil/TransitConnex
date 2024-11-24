using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Infrastructure.Commands.RouteTicket;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouteTicketController(RouteTicketCommandHandler routeTicketCommandHandler) : Controller
{
    [HttpPost]
    public async Task<Guid> CreateRouteTicket(RouteTicketCreateCommand createCommand)
    {
        return await routeTicketCommandHandler.HandleCreate(createCommand);
    }

    [HttpPut]
    public async Task<IActionResult> EditRouteTicket(RouteTicketUpdateCommand updateCommand)
    {
        await routeTicketCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }
    
    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<RouteTicketUpdateCommand> updateCommand)
    {
        // TODO

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRouteTicket(RouteTicketDeleteCommand deleteCommand)
    {
        await routeTicketCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }
    
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteRouteTickets(List<Guid> deleteIds)
    {
        // await RouteTicketCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
