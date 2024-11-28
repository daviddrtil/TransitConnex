using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.RouteTicket;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouteTicketController(RouteTicketCommandHandler routeTicketCommandHandler) : Controller
{
    /// <summary>
    /// Endpoint for creating RouteTicket aka "buying ticket".
    /// </summary>
    /// <param name="createCommand">Command containing info necessary for ticket creation.</param>
    /// <returns>Method status with Id of created command.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Guid> CreateRouteTicket(RouteTicketCreateCommand createCommand)
    {
        return await routeTicketCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for deleting RouteTicket aka "refunding ticket".
    /// </summary>
    /// <param name="id">Id of delted ticket.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRouteTicket(Guid id)
    {
        await routeTicketCommandHandler.HandleDelete(id);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting tickets for given scheduled route - "mass refund" - admin action - only for case of canceling route.
    /// </summary>
    /// <param name="routeId"></param>
    /// <returns></returns>
    [HttpDelete("batch/route/{id}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AuthorizedByAdmin]
    public async Task<IActionResult> DeleteRouteTickets(List<Guid> ids)
    {
        await routeTicketCommandHandler.HandleBatchDelete(ids);
        return Ok();
    }
}
