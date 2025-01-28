using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RouteTicketController(RouteTicketCommandHandler routeTicketCommandHandler, RouteTicketQueryHandler routeTicketQueryHandler) : Controller
{
    [HttpPost("GetRouteTicketsFiltered")]
    [AuthorizedByAdmin]
    public async Task<ActionResult<List<RouteTicketDto>>> GetRouteTicketsFiltered(RouteTicketFilteredQuery filter)
    {
        return Ok(await routeTicketQueryHandler.HandleGetFiltered(filter));
    }
    
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
}
