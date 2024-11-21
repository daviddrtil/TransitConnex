using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Icon;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class IconCommandHandler : IBaseCommandHandler<IIconCommand>
    {
        private readonly IIconService _iconService;

        public IconCommandHandler(IIconService iconService)
        {
            _iconService = iconService;
        }
        
        public Task HandleCreate(IIconCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IIconCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IIconCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
