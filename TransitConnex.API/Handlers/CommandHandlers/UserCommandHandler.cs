using NetTopologySuite.Geometries;
using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class UserCommandHandler(
    IUserService userService,
    IUserFavLocationMongoService locationMongoService,
    IUserFavConnectionMongoService connectionMongoService)
    : IBaseCommandHandler<IUserCommand>
{
    public async Task<Guid> HandleCreate(IUserCommand command)
    {
        if (command is not UserCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected UserCreateCommand.");
        }
        
        var created = await userService.CreateUser(createCommand);
        return created.Id;
    }

    public async Task HandleUpdate(IUserCommand command)
    {
        if (command is not UserUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected UserUpdateCommand.");
        }

        await userService.EditUser(updateCommand);
    }

    public async Task HandleDelete(Guid id)
    {
        await userService.DeleteUser(id);
    }
    
    public async Task HandleRestore(Guid id)
    {
        await userService.RestoreUser(id);
    }

    public async Task HandleLikeLocation(IUserCommand command)
    {
        if (command is not UserLikeLocationCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeLocationCommand)}.");
        }
        
        var location = await userService.LikeLocation(likeCommand);
        await locationMongoService.Add(location);
    }

    public async Task HandleLikeConnection(IUserCommand command)
    {
        if (command is not UserLikeConnectionCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeConnectionCommand)}.");
        }
        
        var conn = await userService.LikeConnection(likeCommand);
        await connectionMongoService.Add(conn);
    }

    public async Task HandleDislikeLocation(IUserCommand command)
    {
        if (command is not UserLikeLocationCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeLocationCommand)}.");
        }
        
        await userService.DislikeLocation(likeCommand);
        var location = new UserLocationFavourite()
        {
            UserId = likeCommand.UserId,
            LocationId = likeCommand.LocationId,
        };
        await locationMongoService.Remove(location);
    }

    public async Task HandleDislikeConnection(IUserCommand command)
    {
        if (command is not UserLikeConnectionCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeConnectionCommand)}.");
        }

        await userService.DislikeConnection(likeCommand);
        var conn = new UserConnectionFavourite()
        {
            UserId = likeCommand.UserId,
            FromLocationId = likeCommand.FromLocationId,
            ToLocationId = likeCommand.ToLocationId,
        };
        await connectionMongoService.Remove(conn);
    }
}
