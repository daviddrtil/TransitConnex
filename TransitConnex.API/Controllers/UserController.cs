using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.User;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    UserCommandHandler userCommandHandler,
    UserQueryHandler userQueryHandler) : Controller
{
    /// <summary>
    /// Endpoint for getting all users from Source of truth.
    /// </summary>
    /// <returns>List of DTOs containing info about Users.</returns>
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllSoT()
    {
        return Ok(await userQueryHandler.HandleGetAll());
    }

    /// <summary>
    /// Endpoint for creating new user - registration. 
    /// </summary>
    /// <param name="createCommand">Command containing all necessary information about user.</param>
    /// <returns>Method status with Id of created user.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreateUser(UserCreateCommand createCommand)
    {
        return Ok(await userCommandHandler.HandleCreate(createCommand));
    }

    /// <summary>
    /// Endpoint for logging user in.
    /// </summary>
    /// <param name="model">TODO</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto model)
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

    /// <summary>
    /// Ednpoint for logging user out.
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUser()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    /// <summary>
    /// Endpoint for editing user - only password edit allowed.
    /// </summary>
    /// <param name="updateCommand">Command containing all updated information about user.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditUser(UserUpdateCommand updateCommand)
    {
        await userCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting user from system - soft delete is performed.
    /// </summary>
    /// <param name="id">Id of deleted user.</param>
    /// <returns>Method status.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await userCommandHandler.HandleDelete(id);
        return Ok();
    }

    /// <summary>
    /// Endpoint for restoring user account after it was soft deleted.
    /// </summary>
    /// <param name="id">Id of restored user.</param>
    /// <returns>Method status.</returns>
    [HttpPost("restore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RestoreUser(Guid id)
    {
        await userCommandHandler.HandleRestore(id);
        return Ok();
    }

    [Authorize]
    [HttpGet("GetFavouriteLocations")]
    public async Task<IEnumerable<UserFavLocationDto>> GetFavouriteLocations()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return [];
        return await userQueryHandler.HandleGetFavouriteLocations(user.Id);
    }

    [Authorize]
    [HttpGet("GetFavouriteConnections")]
    public async Task<IEnumerable<UserFavConnectionDto>> GetFavouriteConnections()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return [];
        return await userQueryHandler.HandleGetFavouriteConnections(user.Id);
    }

    [Authorize]
    [HttpGet("GetSearchedRoutes")]
    public async Task<IEnumerable<SearchedRouteDto>> GetSearchedRoutes()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return [];
        return await userQueryHandler.HandleGetSearchedRoutes(user.Id);
    }

    /// <summary>
    /// Endpoint for creating new users favourite connection between two locations.
    /// </summary>
    /// <param name="command">Command containing ids of from/to location.</param>
    /// <returns>Method status.</returns>
    [Authorize]
    [HttpPost("like-line")]
    public async Task<IActionResult> LikeConnection(UserLikeConnectionCommand command)
    {
        var user = await userManager.GetUserAsync(User);
        command.UserId = user!.Id;   // todo remove property UserId from command
        await userCommandHandler.HandleLikeConnection(command);
        return Ok();
    }

    /// <summary>
    /// Endpoint for creating users new favourite location for location suggestions.
    /// </summary>
    /// <param name="command">Command containing location id.</param>
    /// <returns>Method status.</returns>
    [Authorize]
    [HttpPost("like-location")]
    public async Task<IActionResult> LikeLocation(UserLikeLocationCommand command)
    {
        await userCommandHandler.HandleLikeLocation(command);
        return Ok();
    }

    /// <summary>
    /// Endpoint for removing connection from users favourites.
    /// </summary>
    /// <param name="command">Command containing ids of from/to location.</param>
    /// <returns>Method status.</returns>
    [Authorize]
    [HttpDelete("dislike-line")]
    public async Task<IActionResult> DislikeConnection(UserLikeConnectionCommand command)
    {
        await userCommandHandler.HandleDislikeConnection(command);
        return Ok();
    }

    /// <summary>
    /// Endpoint for removing location from users favourites.
    /// </summary>
    /// <param name="command">Command containing location id.</param>
    /// <returns>Method status.</returns>
    [Authorize]
    [HttpDelete("dislike-location")]
    public async Task<IActionResult> DislikeLocation(UserLikeLocationCommand command)
    {
        await userCommandHandler.HandleDislikeLocation(command);
        return Ok();
    }
}
