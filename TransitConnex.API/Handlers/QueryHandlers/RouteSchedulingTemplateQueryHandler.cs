using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class RouteSchedulingTemplateQueryHandler(IRouteSchedulingTemplateService routeSchedulingTemplateServiceSoT) : IBaseQueryHandler<RouteSchedulingTemplateDto>
{
    public async Task<List<RouteSchedulingTemplateDto>> HandleGetFiltered(RouteSchedulingTemplateFilteredQuery filter)
    {
        return await routeSchedulingTemplateServiceSoT.GetFilteredRouteSchedulingTemplates(filter);
    }

    public async Task<RouteSchedulingTemplateDto> HandleGetById(Guid routeSchedulingTemplateId)
    {
        return await routeSchedulingTemplateServiceSoT.GetRouteSchedulingTemplateById(routeSchedulingTemplateId);
    }
}
