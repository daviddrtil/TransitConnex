using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.User;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserCommandHandler userCommandHandler, SignInManager<User> signInManager) : Controller
{
    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        // TODO -> Query handler

        return null;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Guid>> CreateUser(UserCreateCommand createCommand)
    { 
        return Ok(await userCommandHandler.HandleCreate(createCommand));
    }
    
    [HttpPost("restore")]
    public async Task<IActionResult> RestoreUser(Guid id)
    {
        await userCommandHandler.HandleRestore(id);

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto model) // TODO -> switch to queryHandler + add check for deleted user
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                bool isAdmin = false;
                if (User.Identity is { IsAuthenticated: true })
                    isAdmin = User.IsInRole("Admin");
                return Ok(new { isAdmin });
            }
            else
            {
                return Unauthorized();
            }
        }
        return BadRequest(ModelState);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUser()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("like-line")]
    public async Task<IActionResult> LikeConnection(UserLikeConnectionCommand command)
    {
        await userCommandHandler.HandleLikeConnection(command);
        
        return Ok();
    }

    [HttpPost("like-location")]
    public async Task<IActionResult> LikeLocation(UserLikeLocationCommand command)
    {
        await userCommandHandler.HandleLikeLocation(command);

        return Ok();
    }
    
    [HttpDelete("dislike-line")]
    public async Task<IActionResult> DislikeConnection(UserLikeConnectionCommand command)
    {
        await userCommandHandler.HandleDislikeConnection(command);
        
        return Ok();
    }

    [HttpDelete("dislike-location")]
    public async Task<IActionResult> DislikeLocation(UserLikeLocationCommand command)
    {
        await userCommandHandler.HandleDislikeLocation(command);

        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> EditUser(UserUpdateCommand updateCommand)
    {
        await userCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await userCommandHandler.HandleDelete(id);

        return Ok();
    }
}
