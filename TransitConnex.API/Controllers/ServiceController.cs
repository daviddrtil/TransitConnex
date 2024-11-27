using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Service;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizedByAdmin]
public class ServiceController(ServiceCommandHandler serviceCommandHandler) : Controller
{
    /// <summary>
    /// Endpoint for creating new service.
    /// </summary>
    /// <param name="createCommand">Command containing all necessary information about service.</param>
    /// <returns>Method status with Id of created service.</returns>
    [HttpPost]
    public async Task<Guid> CreateService(ServiceCreateCommand createCommand)
    {
        return await serviceCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for editing service.
    /// </summary>
    /// <param name="updateCommand">Command containing all updated information about service.</param>
    /// <returns>Method status</returns>
    [HttpPut]
    public async Task<IActionResult> EditService(ServiceUpdateCommand updateCommand)
    {
        await serviceCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting service from system.
    /// </summary>
    /// <param name="id">Id of deleted service.</param>
    /// <returns>Method status</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteService(Guid id)
    {
        await serviceCommandHandler.HandleDelete(id); 

        return Ok();
    }
}
