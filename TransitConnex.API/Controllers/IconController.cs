using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Domain.DTOs.Icon;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class IconController(IconCommandHandler iconCommandHandler) : Controller
{
    [HttpGet]
    public async Task<List<IconDto>> GetAll()
    {
        // TODO -> query handler
        return null;
    }

    /// <summary>
    /// Endpoint for creating new icon.
    /// </summary>
    /// <param name="createCommand">Command containing all necessary information about icon.</param>
    /// <returns>Method status with Id of created icon.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateIcon(IconCreateCommand createCommand)
    {
        return Ok(await iconCommandHandler.HandleCreate(createCommand));
    }

    /// <summary>
    /// Endpoint for editing icon.
    /// </summary>
    /// <param name="updateCommand">Command containing all updated information about icon.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    public async Task<IActionResult> EditIcon(IconUpdateCommand updateCommand)
    {
        await iconCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting icon from the system.
    /// </summary>
    /// <param name="id">Id of deleted icon</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIcon(Guid id)
    {
        await iconCommandHandler.HandleDelete(id);

        return Ok();
    }
}
