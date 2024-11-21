using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Location;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class LocationCommandHandler : IBaseCommandHandler<ILocationCommand>
    {
        private readonly ILocationService _locationService;

        public LocationCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }
        
        public Task HandleCreate(ILocationCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(ILocationCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(ILocationCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
