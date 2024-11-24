using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteSchedulingTemplateService
{
    Task<List<RouteSchedulingTemplateDto>> GetAllRouteSchedulingTemplates();

    Task<RouteSchedulingTemplateDto> GetRouteSchedulingTemplateById(Guid id);

    Task<bool> RouteSchedulingTemplateExists(Guid id);

    Task<RouteSchedulingTemplateDto> CreateRouteSchedulingTemplate(
        RouteSchedulingTemplateCreateDto routeSchedulingTemplateDto);

    Task<RouteSchedulingTemplateDto> EditRouteSchedulingTemplate(Guid id,
        RouteSchedulingTemplateCreateDto editedRouteSchedulingTemplate);

    Task DeleteRouteSchedulingTemplate(Guid id);
}
