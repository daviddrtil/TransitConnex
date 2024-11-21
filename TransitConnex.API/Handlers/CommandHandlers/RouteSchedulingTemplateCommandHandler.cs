using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.RouteSchedulingTemplate;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class RouteSchedulingTemplateCommandHandler : IBaseCommandHandler<IRouteSchedulingTemplateCommand>
    {
        private readonly IRouteSchedulingTemplateService _routeSchedulingTemplateService;

        public RouteSchedulingTemplateCommandHandler(IRouteSchedulingTemplateService routeSchedulingTemplateService)
        {
            _routeSchedulingTemplateService = routeSchedulingTemplateService;
        }
        
        public Task HandleCreate(IRouteSchedulingTemplateCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(IRouteSchedulingTemplateCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(IRouteSchedulingTemplateCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
