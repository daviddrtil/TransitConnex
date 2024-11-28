using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class LocationController(
    LocationCommandHandler locationCommandHandler,
    LocationQueryHandler locationQueryHandler) : Controller
{
    [HttpGet("GetById/{id}")]
    public async Task<LocationDto?> GetById(Guid id)
    {
        return await locationQueryHandler.HandleGetById(id);
    }

    [HttpGet("GetByName")]
    public async Task<IEnumerable<LocationDto>> GetByName([Required] string name)
    {
        var query = new LocationGetByNameQuery(name);
        return await locationQueryHandler.HandleGetByName(query);
    }

    [HttpGet("GetClosest")]
    public async Task<LocationDto?> GetClosestLocation([Required] double longitude, [Required] double latitude)
    {
        var query = new LocationGetClosestQuery(longitude, latitude);
        return await locationQueryHandler.HandleGetClosest(query);
    }

    /// <summary>
    /// Endpoint for creating new Location.
    /// </summary>
    /// <param name="createCommand">Command containing all needed information for creating new location.</param>
    /// <returns>Method status with Id of created location.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLocation(LocationCreateCommand createCommand)
    {
        return Ok(await locationCommandHandler.HandleCreate(createCommand));
    }

    /// <summary>
    /// Endpoint for creating multiple new Locations.
    /// </summary>
    /// <param name="createCommands">Command containing list of commands for creating location.</param>
    /// <returns>Method status with Ids of created locations.</returns>
    [HttpPost("batch")]
    public async Task<ActionResult<List<Guid>>> CreateLocations(LocationBatchCreateCommand createCommands)
    {
        return Ok(await locationCommandHandler.HandleBatchCreate(createCommands));
    }

    /// <summary>
    /// Endpoint for editing Location.
    /// </summary>
    /// <param name="updateCommand">Command containing all updated information about location.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    public async Task<IActionResult> EditLocation(LocationUpdateCommand updateCommand)
    {
        await locationCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting Location.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteLocation(Guid id) // TODO -> stopky unassignout? nebo smazat? -> opet serious action
    {
        await locationCommandHandler.HandleDelete(id);

        return Ok();
    }

    /// <summary>
    /// Endpoint for
    /// </summary>
    /// <param name="deleteIds"></param>
    /// <returns></returns>
    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteLocations(List<Guid> deleteIds)
    {
        // await LocationCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
