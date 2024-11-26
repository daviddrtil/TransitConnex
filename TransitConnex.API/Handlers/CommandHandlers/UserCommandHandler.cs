using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class UserCommandHandler(IUserService userService) : IBaseCommandHandler<IUserCommand>
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
        
        await userService.LikeLocation(likeCommand);
        // TODO -> sync with mongo
    }

    public async Task HandleLikeConnection(IUserCommand command)
    {
        if (command is not UserLikeConnectionCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeConnectionCommand)}.");
        }
        
        await userService.LikeConnection(likeCommand);
        // TODO -> sync with mongo
    }
    
    public async Task HandleDislikeLocation(IUserCommand command)
    {
        if (command is not UserLikeLocationCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeLocationCommand)}.");
        }
        
        await userService.DislikeLocation(likeCommand);
        // TODO -> sync with mongo
    }

    public async Task HandleDislikeConnection(IUserCommand command)
    {
        if (command is not UserLikeConnectionCommand likeCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(UserLikeConnectionCommand)}.");
        }
        
        await userService.DislikeConnection(likeCommand);
        // TODO -> sync with mongo
    }
}
