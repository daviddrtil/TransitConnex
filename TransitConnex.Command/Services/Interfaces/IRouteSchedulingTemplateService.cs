using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteSchedulingTemplateService
{
    Task<List<RouteSchedulingTemplateDto>> GetAllRouteSchedulingTemplates();
    Task<RouteSchedulingTemplateDto> GetRouteSchedulingTemplateById(Guid id);
    

    Task<RouteSchedulingTemplate> CreateRouteSchedulingTemplate(RouteSchedulingTemplateCreateCommand createCommand);
    Task<RouteSchedulingTemplate> EditRouteSchedulingTemplate(RouteSchedulingTemplateUpdateCommand editCommand);
    Task DeleteRouteSchedulingTemplate(Guid id);
    Task RunScheduler(RouteSchedulingTemplateRunSchedulerCommand runCommand);
}
