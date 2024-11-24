using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;

namespace TransitConnex.Command.Services;

public class RouteSchedulingTemplateService(IRouteSchedulingTemplateRepository routeSchedulingTemplateRepository)
    : IRouteSchedulingTemplateService
{
    public Task<List<RouteSchedulingTemplateDto>> GetAllRouteSchedulingTemplates()
    {
        throw new NotImplementedException();
    }

    public Task<RouteSchedulingTemplateDto> GetRouteSchedulingTemplateById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RouteSchedulingTemplateExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RouteSchedulingTemplateDto> CreateRouteSchedulingTemplate(
        RouteSchedulingTemplateCreateDto routeSchedulingTemplateDto)
    {
        throw new NotImplementedException();
    }

    public Task<RouteSchedulingTemplateDto> EditRouteSchedulingTemplate(Guid id,
        RouteSchedulingTemplateCreateDto editedRouteSchedulingTemplate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRouteSchedulingTemplate(Guid id)
    {
        throw new NotImplementedException();
    }
}
