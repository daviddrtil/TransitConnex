using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class ScheduledRouteQueryHandler(
    IMapper mapper,
    IScheduledRouteMongoService scheduledRouteService) : IBaseQueryHandler<ScheduledRouteDto>
{
    public async Task<IEnumerable<ScheduledRouteDto>> HandleGetScheduledRoutes(
        ScheduledRouteGetAllQuery query)
    {
        return await scheduledRouteService.GetScheduledRoutes(
            query.StartLocationId, query.EndLocationId, query.StartTime);
    }
}
