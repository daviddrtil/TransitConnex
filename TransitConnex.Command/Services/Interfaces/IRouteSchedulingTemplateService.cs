using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteSchedulingTemplateService
{
    Task<List<RouteSchedulingTemplateDto>> GetFilteredRouteSchedulingTemplates(RouteSchedulingTemplateFilteredQuery filter);
    Task<RouteSchedulingTemplateDto> GetRouteSchedulingTemplateById(Guid id);
    

    Task<RouteSchedulingTemplate> CreateRouteSchedulingTemplate(RouteSchedulingTemplateCreateCommand createCommand);
    Task<RouteSchedulingTemplate> EditRouteSchedulingTemplate(RouteSchedulingTemplateUpdateCommand editCommand);
    Task DeleteRouteSchedulingTemplate(Guid id);
    Task DeleteRouteSchedulingTemplatesForRoute(Guid routeId);
    Task RunScheduler(RouteSchedulingTemplateRunSchedulerCommand runCommand);
}
