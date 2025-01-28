using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
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
    public async Task<LocationDto?> GetClosestLocation([Required] double latitude, [Required] double longitude)
    {
        var query = new LocationGetClosestQuery(latitude, longitude);
        return await locationQueryHandler.HandleGetClosest(query);
    }

    [HttpPost("filter")]
    public async Task<ActionResult<List<LocationDto>>> GetLocationsFilteredSoT(LocationFilteredQuery filter)
    {
        return Ok(await locationQueryHandler.HandleGetFiltered(filter));
    }
    
    /// <summary>
    /// Endpoint for creating new Location.
    /// </summary>
    /// <param name="createCommand">Command containing all needed information for creating new location.</param>
    /// <returns>Method status with Id of created location.</returns>
    [HttpPost]
    [AuthorizedByAdmin]
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
    [AuthorizedByAdmin]
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
    [AuthorizedByAdmin]
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
    [HttpDelete("{id}")]
    [AuthorizedByAdmin]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        await locationCommandHandler.HandleDelete(id);
        return Ok();
    }
}
