using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RouteTicketController(RouteTicketCommandHandler routeTicketCommandHandler) : Controller
{
    //[AuthorizedByAdmin]
    //public async Task<List<RouteTicketDto>> GetRouteTicketsFiltered()
    //{
    //    throw new NotImplementedException();
    //}
    
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
