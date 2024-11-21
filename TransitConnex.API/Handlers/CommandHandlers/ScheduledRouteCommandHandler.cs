using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.ScheduledRoute;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class ScheduledRouteCommandHandler : IBaseCommandHandler<IScheduledRouteCommand>
    {
        private readonly IScheduledRouteService _scheduledRouteService;

        public ScheduledRouteCommandHandler(IScheduledRouteService scheduledRouteService)
        {
            _scheduledRouteService = scheduledRouteService;
        }
        
        public Task HandleCreate(IScheduledRouteCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IScheduledRouteCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IScheduledRouteCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
