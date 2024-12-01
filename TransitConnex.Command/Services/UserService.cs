using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class UserService(IMapper mapper, IUserRepository userRepository, ILocationRepository locationRepository, ILineRepository lineRepository, UserManager<User> userManager) : IUserService
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await userRepository.QueryAll().ToListAsync();
        return mapper.Map<List<UserDto>>(users);
    }

    public async Task<User> CreateUser(UserCreateCommand createCommand)
    {
        if (await userRepository.EmailExists(createCommand.Email))
        {
            throw new ArgumentException($"Email {createCommand.Email} already exists.");
        }

        if (createCommand.Password != createCommand.ConfirmPassword) 
        {
            throw new ArgumentException("Passwords do not match.");
        }

        var newUser = mapper.Map<User>(createCommand);
        var result = await userManager.CreateAsync(newUser, createCommand.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }  
        
        return newUser;
    }

    public async Task<User> EditUser(UserUpdateCommand updateCommand)
    {
        var user = await userRepository.QueryById(updateCommand.Id).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new ArgumentException($"User with id {updateCommand.Id} does not exist");
        }
        
        var res = await userManager.ChangePasswordAsync(user, updateCommand.OldPassword, updateCommand.NewPassword);
        if (!res.Succeeded)
        {
            throw new ArgumentException(res.Errors.First().Description);
        }
        
        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await userRepository.QueryById(id).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new KeyNotFoundException($"Icon with ID {id} was not found.");
        }

        user.Deleted = true;
        user.Updated = DateTime.Now;
        
        await userRepository.Update(user);
    }

    public async Task RestoreUser(Guid id)
    {
        var user = await userRepository.QueryById(id).FirstOrDefaultAsync();
        
        if (user == null)
        {
            throw new KeyNotFoundException($"Icon with ID {id} was not found.");
        }
        
        user.IsAdmin = true;
        user.Updated = DateTime.Now;
        
        await userRepository.Update(user);
    }

    public async Task<UserLocationFavourite> LikeLocation(UserLikeLocationCommand likeCommand)
    {
        if (!await userRepository.Exists(likeCommand.UserId))
        {
            throw new KeyNotFoundException($"Icon with ID {likeCommand.UserId} was not found.");
        }
        
        if (!await locationRepository.Exists(likeCommand.LocationId))
        {
            throw new KeyNotFoundException($"Location with ID {likeCommand.LocationId} was not found.");
        }
        
        var like = new UserLocationFavourite() {UserId = likeCommand.UserId, LocationId = likeCommand.LocationId};

        await userRepository.AddUserLocationFavourite(like);
        return like;
    }

    public async Task<UserConnectionFavourite> LikeConnection(UserLikeConnectionCommand likeCommand)
    {
        if (!await userRepository.Exists(likeCommand.UserId))
        {
            throw new KeyNotFoundException($"Icon with ID {likeCommand.UserId} was not found.");
        }
        
        if (!await locationRepository.Exists(likeCommand.FromLocationId))
        {
            throw new KeyNotFoundException($"Location with ID {likeCommand.FromLocationId} was not found.");
        }
        
        if (!await locationRepository.Exists(likeCommand.ToLocationId))
        {
            throw new KeyNotFoundException($"Location with ID {likeCommand.ToLocationId} was not found.");
        }
        
        var like = new UserConnectionFavourite()
        {
            UserId = likeCommand.UserId,
            FromLocationId = likeCommand.FromLocationId,
            ToLocationId = likeCommand.ToLocationId,
        };
        
        await userRepository.AddUserLineFavourite(like);
        return like;
    }

    public async Task DislikeLocation(UserLikeLocationCommand likeCommand)
    {
        var like = await userRepository.QueryLocationFavouritesByIds(likeCommand.UserId,likeCommand.LocationId).FirstOrDefaultAsync();
        if (like == null)
        {
            throw new KeyNotFoundException($"Given line like was not found.");
        }

        await userRepository.DeleteUserLocationFavourite(like);
    }

    public async Task DislikeConnection(UserLikeConnectionCommand likeCommand)
    {
        var like = await userRepository.QueryConnectionFavouritesByIds(likeCommand.UserId,likeCommand.FromLocationId, likeCommand.ToLocationId).FirstOrDefaultAsync();
        if (like == null)
        {
            throw new KeyNotFoundException($"Given line like was not found.");
        }

        await userRepository.DeleteUserConnectionFavourite(like);
    }
}
