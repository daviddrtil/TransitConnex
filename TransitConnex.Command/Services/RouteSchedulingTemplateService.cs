using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services;

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
