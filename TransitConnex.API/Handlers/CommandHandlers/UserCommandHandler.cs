using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.User;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class UserCommandHandler : IBaseCommandHandler<IUserCommand>
    {
        private readonly IUserService _userService;

        public UserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public Task HandleCreate(IUserCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IUserCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
