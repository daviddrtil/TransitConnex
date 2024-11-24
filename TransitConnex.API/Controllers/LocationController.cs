using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Location;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController(LocationCommandHandler locationCommandHandler) : Controller
{
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
