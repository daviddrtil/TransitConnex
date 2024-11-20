using TransitConnex.Domain.DTOs.User;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        
        Task<UserDto> GetUserById(Guid id);
        
        Task<bool> UserExists(Guid id);
        
        Task<UserDto> CreateUser(UserCreateDto userDto);

        Task<UserDto> EditUser(Guid id, UserCreateDto editedUser);
        
        Task DeleteUser(Guid id);
    }
}
