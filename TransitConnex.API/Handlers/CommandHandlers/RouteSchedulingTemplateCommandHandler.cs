using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class RouteSchedulingTemplateCommandHandler(
    IRouteSchedulingTemplateService routeSchedulingTemplateService,
    IScheduledRouteService srService,
    IScheduledRouteMongoService srMongoService)
        : IBaseCommandHandler<IRouteSchedulingTemplateCommand>
{
    public async Task<Guid> HandleCreate(IRouteSchedulingTemplateCommand command)
    {
        if (command is not RouteSchedulingTemplateCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(RouteSchedulingTemplateCreateCommand)}.");
        }
        var created = await routeSchedulingTemplateService.CreateRouteSchedulingTemplate(createCommand);
        return created.Id;
    }

    public async Task HandleUpdate(IRouteSchedulingTemplateCommand command)
    {
        if (command is not RouteSchedulingTemplateUpdateCommand updateCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(RouteSchedulingTemplateUpdateCommand)}.");
        }
        await routeSchedulingTemplateService.EditRouteSchedulingTemplate(updateCommand);
    }

    public async Task HandleDelete(Guid id) 
    {
        await routeSchedulingTemplateService.DeleteRouteSchedulingTemplate(id);
    }

    public async Task HandleDeleteForRoute(Guid routeId)
    {
        await routeSchedulingTemplateService.DeleteRouteSchedulingTemplatesForRoute(routeId);
    }

    public async Task HandleScheduler(IRouteSchedulingTemplateCommand command)
    {
        if (command is not RouteSchedulingTemplateRunSchedulerCommand runCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(RouteSchedulingTemplateRunSchedulerCommand)}.");
        }
        await routeSchedulingTemplateService.RunScheduler(runCommand);

        if (runCommand.RouteIds is null)
            return;
        foreach (var routeId in runCommand.RouteIds)
        {
            var srs = await srService.GetAllByRouteId(routeId);
            await srMongoService.Update(srs);
        }
    }
}
