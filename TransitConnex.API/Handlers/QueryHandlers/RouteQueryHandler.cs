using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Route;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class RouteQueryHandler(IRouteService routeService) : IBaseQueryHandler<RouteDto>
{
    public async Task<List<RouteDto>> HandleGetFiltered(RouteFilteredQuery filter)
    {
        return await routeService.GetRoutesFiltered(filter);
    }
}
