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
    public async Task<Guid> CreateLocation(LocationCreateCommand createCommand)
    {
        return await locationCommandHandler.HandleCreate(createCommand);
    }

    [HttpPost("batch")]
    public async Task<Guid> CreateLocations(List<LocationCreateCommand> createCommands)
    {
        // return await LocationCommandHandler.HandleCreate(createCommand);
        return new Guid(); // TODO
    }

    [HttpPut]
    public async Task<IActionResult> EditLocation(LocationUpdateCommand updateCommand)
    {
        await locationCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLocation(LocationDeleteCommand deleteCommand)
    {
        await locationCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteLocations(List<Guid> deleteIds)
    {
        // await LocationCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
