using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class LocationQueryHandler(
    IMapper mapper,
    ILocationMongoService locationService) : IBaseQueryHandler<LocationDto>
{
    public async Task<IEnumerable<LocationDto>> HandleGetByName(LocationGetByNameQuery query)
    {
        var locations = await locationService.GetByName(query.Name);
        return mapper.Map<IEnumerable<LocationDto>>(locations);
    }

    public async Task<LocationDto?> HandleGetClosest(LocationGetClosestQuery query)
    {
        var location = await locationService.GetClosest(
            query.Latitude, query.Longitude);
        if (location == null)
            return null;
        return mapper.Map<LocationDto>(location);
    }
}
