using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Service;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController(ServiceCommandHandler serviceCommandHandler) : Controller
{
    [HttpPost]
    public async Task<Guid> CreateService(ServiceCreateCommand createCommand)
    {
        return await serviceCommandHandler.HandleCreate(createCommand);
    }

    [HttpPut]
    public async Task<IActionResult> EditService(ServiceUpdateCommand updateCommand)
    {
        await serviceCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteService(Guid id)
    {
        await serviceCommandHandler.HandleDelete(id); 

        return Ok();
    }
}
