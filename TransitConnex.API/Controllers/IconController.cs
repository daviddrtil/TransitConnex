using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Infrastructure.Commands.Icon;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IconController(IconCommandHandler iconCommandHandler) : Controller
{
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

    [HttpDelete]
    public async Task<IActionResult> DeleteIcon(IconDeleteCommand deleteCommand)
    {
        await iconCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }
}
