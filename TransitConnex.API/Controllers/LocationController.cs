using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController(
    LocationCommandHandler locationCommandHandler,
    LocationQueryHandler locationQueryHandler) : Controller
{
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

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLocation(LocationCreateCommand createCommand)
    {
        return Ok(await locationCommandHandler.HandleCreate(createCommand));
    }

    [HttpPost("batch")]
    public async Task<ActionResult<List<Guid>>> CreateLocations(LocationBatchCreateCommand createCommands)
    {
        return Ok(await locationCommandHandler.HandleBatchCreate(createCommands));
    }

    [HttpPut]
    public async Task<IActionResult> EditLocation(LocationUpdateCommand updateCommand)
    {
        await locationCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        await locationCommandHandler.HandleDelete(id);

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteLocations(List<Guid> deleteIds)
    {
        // await LocationCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
