using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Service;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class ServiceCommandHandler : IBaseCommandHandler<IServiceCommand>
    {
        private readonly IServiceService _serviceService;

        public ServiceCommandHandler(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        
        public Task HandleCreate(IServiceCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IServiceCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IServiceCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
