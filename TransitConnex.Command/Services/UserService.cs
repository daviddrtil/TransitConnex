using TransitConnex.Domain.DTOs.User;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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

        public Task<UserDto> CreateUser(UserCreateDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> EditUser(Guid id, UserCreateDto editedUser)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
