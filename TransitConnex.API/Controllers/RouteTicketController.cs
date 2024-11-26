using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.RouteTicket;

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRouteTicket(Guid id)
    {
        await routeTicketCommandHandler.HandleDelete(id);

        return Ok();
    }

    [HttpDelete("batch")] 
    public async Task<IActionResult> DeleteRouteTickets(List<Guid> deleteIds)
    {
        // await RouteTicketCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
