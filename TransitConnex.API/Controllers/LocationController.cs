using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController(
    LocationCommandHandler locationCommandHandler,
    LocationQueryHandler locationQueryHandler) : Controller
{
    [HttpGet("ByName")]
    public async Task<IEnumerable<LocationDto>> GetByName(string name)
    {
        return await locationQueryHandler.HandleGetByName(name);
    }

    [HttpGet("Closest")]
    public async Task<LocationDto> GetClosestLocation(double latitude, double longitude)
    {
        return await locationQueryHandler.HandleGetClosest(latitude, longitude);
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
