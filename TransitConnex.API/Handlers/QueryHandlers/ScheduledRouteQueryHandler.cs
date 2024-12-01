using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class ScheduledRouteQueryHandler(
    IMapper mapper,
    IScheduledRouteMongoService scheduledRouteService,
    ISearchedRouteMongoService searchedRouteService,
    IScheduledRouteService scheduledRouteServiceSoT
    ) : IBaseQueryHandler<ScheduledRouteDto>
{
    public async Task<IEnumerable<ScheduledRouteDto>> HandleGetScheduledRoutes(
        ScheduledRouteGetAllQuery query)
    {
        var scheduledRoutes = await scheduledRouteService.GetScheduledRoutes(
            query.StartLocationId, query.EndLocationId, query.StartTime);
        var searchedRoute = new SearchedRouteDto()
        {
            UserId = query.UserId,
            FromLocationId = query.StartLocationId,
            ToLocationId = query.EndLocationId,
            SearchTime = query.StartTime,
            ScheduledRouteIds = scheduledRoutes.Select(sr => sr.Id).ToList(),
        };
        await searchedRouteService.Create(searchedRoute);
        return scheduledRoutes;
    }

    public async Task<List<ScheduledRouteDto>> HandleGetScheduledRoutesFiltered(ScheduledRouteFilteredQuery filter)
    {
        return await scheduledRouteServiceSoT.GetScheduledRoutesFiltered(filter);
    }
}
