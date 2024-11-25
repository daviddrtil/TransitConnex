using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class LocationQueryHandler(
    IMapper mapper,
    ILocationMongoService locationService) : IBaseQueryHandler<LocationDto>
{
    public async Task<IEnumerable<LocationDto>> HandleGetByName(string name)
    {
        var locations = await locationService.GetByName(name);
        return mapper.Map<IEnumerable<LocationDto>>(locations);
    }

    public async Task<LocationDto?> HandleGetClosest(double latitude, double longitude)
    {
        var location = await locationService.GetClosest(latitude, longitude);
        if (location == null)
            return null;
        return mapper.Map<LocationDto>(location);
    }
}
