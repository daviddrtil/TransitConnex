using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.User;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class UserCommandHandler(IUserService userService) : IBaseCommandHandler<IUserCommand>
{
    public async Task<Guid> HandleCreate(IUserCommand command)
    {
        if (command is not UserCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected UserCreateCommand.");
        }

        // TODO -> validation for already existing user? -> prolly in service

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

    public async Task HandleDelete(IUserCommand command)
    {
        if (command is not UserDeleteCommand deleteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected UserDeleteCommand.");
        }

        await userService.DeleteUser(deleteCommand);
    }
}
