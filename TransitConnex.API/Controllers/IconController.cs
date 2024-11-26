using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Domain.DTOs.Icon;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IconController(IconCommandHandler iconCommandHandler) : Controller
{
    [HttpGet]
    public async Task<List<IconDto>> GetAll()
    {
        // TODO -> query handler
        return null;
    }

    [HttpPost]
    public async Task<Guid> CreateIcon(IconCreateCommand createCommand)
    {
        return await iconCommandHandler.HandleCreate(createCommand);
    }

    [HttpPut]
    public async Task<IActionResult> EditIcon(IconUpdateCommand updateCommand)
    {
        await iconCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIcon(Guid id)
    {
        await iconCommandHandler.HandleDelete(id);

        return Ok();
    }
}
