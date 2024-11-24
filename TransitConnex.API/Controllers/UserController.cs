using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.User;
using TransitConnex.Domain.DTOs.User;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserCommandHandler userCommandHandler) : Controller
{
    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        // TODO -> Query handler

        return null;
    }

    [HttpPost("register")]
    public async Task<Guid> CreateUser(UserCreateCommand createCommand)
    {
        var created = await userCommandHandler.HandleCreate(createCommand);

        return new Guid();
    }

    [HttpPost("login")]
    public async Task LoginUser()
    {
        // TODO -> Query handler
    }

    [HttpPost("logout")]
    public async Task LogoutUser()
    {
        // TODO -> Query handler
    }

    [HttpPut]
    public async Task<IActionResult> EditUser(UserUpdateCommand updateCommand)
    {
        await userCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(UserDeleteCommand deleteCommand)
    {
        await userCommandHandler.HandleDelete(deleteCommand);

        return Ok();
    }
}
