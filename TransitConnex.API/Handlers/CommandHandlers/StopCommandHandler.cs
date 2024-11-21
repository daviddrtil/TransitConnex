using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Stop;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class StopCommandHandler : IBaseCommandHandler<IStopCommand>
    {
        private readonly IStopService _stopService;

        public StopCommandHandler(IStopService stopService)
        {
            _stopService = stopService;
        }
        
        public Task HandleCreate(IStopCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IStopCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IStopCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
