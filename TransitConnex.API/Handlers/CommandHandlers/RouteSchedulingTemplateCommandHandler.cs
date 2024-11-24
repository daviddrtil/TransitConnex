using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteSchedulingTemplateCommandHandler(IRouteSchedulingTemplateService routeSchedulingTemplateService)
    : IBaseCommandHandler<IRouteSchedulingTemplateCommand>
{
    private readonly IRouteSchedulingTemplateService _routeSchedulingTemplateService = routeSchedulingTemplateService;

    public async Task<Guid> HandleCreate(IRouteSchedulingTemplateCommand command)
    {
        if (command is not RouteSchedulingTemplateCreateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteSchedulingtemplateCreateCommand.");
        }

        return new Guid();
    }

    public async Task HandleUpdate(IRouteSchedulingTemplateCommand command)
    {
        if (command is not RouteSchedulingTemplateUpdateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteSchedulingtemplateUpdateCommand.");
        }
    }

    public async Task HandleDelete(IRouteSchedulingTemplateCommand command)
    {
        if (command is not RouteSchedulingTemplateDeleteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected RouteSchedulingtemplateDeleteCommand.");
        }
    }
}
