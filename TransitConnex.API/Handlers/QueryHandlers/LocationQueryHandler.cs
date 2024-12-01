using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class LocationQueryHandler(
    ILocationMongoService locationService,
    ILocationService locationServiceSoT) : IBaseQueryHandler<LocationDto>
{
    public async Task<LocationDto?> HandleGetById(Guid id)
    {
        return await locationService.GetById(id);
    }

    public async Task<IEnumerable<LocationDto>> HandleGetByName(LocationGetByNameQuery query)
    {
        return await locationService.GetByName(query.Name);
    }

    public async Task<LocationDto?> HandleGetClosest(LocationGetClosestQuery query)
    {
        return await locationService.GetClosest(query.Latitude, query.Longitude);
    }

    public async Task<List<LocationDto>> HandleGetFiltered(LocationFilteredQuery filter)
    {
        return await locationServiceSoT.GetLocationsFiltered(filter);
    }
}
