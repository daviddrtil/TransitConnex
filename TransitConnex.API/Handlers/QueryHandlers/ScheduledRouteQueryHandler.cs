using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class ScheduledRouteQueryHandler(
    IMapper mapper,
    IScheduledRouteMongoService scheduledRouteService) : IBaseQueryHandler<ScheduledRouteDto>
{
}
