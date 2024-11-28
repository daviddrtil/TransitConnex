using TransitConnex.Command.Commands.User;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IUserService
{
    // SoT queries
    Task<List<UserDto>> GetAllUsers();

    // Commands
    Task<User> CreateUser(UserCreateCommand createCommand);
    Task<User> EditUser(UserUpdateCommand updateCommand);
    Task DeleteUser(Guid id);
    Task RestoreUser(Guid id);
    
    Task LikeLocation(UserLikeLocationCommand likeCommand);
    Task LikeConnection(UserLikeConnectionCommand likeCommand);
    Task DislikeLocation(UserLikeLocationCommand likeCommand);
    Task DislikeConnection(UserLikeConnectionCommand likeCommand);
}
