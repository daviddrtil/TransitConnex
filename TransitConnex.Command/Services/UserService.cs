using MongoDB.Driver.Linq;
using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<List<UserDto>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateUser(UserCreateCommand createCommand)
    {
        throw new NotImplementedException();
    }

    public User EditUser(UserUpdateCommand updateCommand)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUser(UserDeleteCommand deleteCommand)
    {
        var user = await userRepository.QueryById(deleteCommand.Id).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException($"Icon with ID {deleteCommand.Id} was not found.");
        }

        await userRepository.Delete(user);
    }

    Task<User> IUserService.CreateUser(UserCreateCommand createCommand)
    {
        throw new NotImplementedException();
    }

    Task<User> IUserService.EditUser(UserUpdateCommand updateCommand)
    {
        throw new NotImplementedException();
    }
}
