using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.DTOs.SearchedRoute;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.DTOs.VehicleRTI;

namespace TransitConnex.Domain.Automapping;

public class MappingProfile : Profile
{
    private void MapModels()
    {
        // todo
    }

    private void MapCollections()
    {
        CreateMap<LocationDoc, LocationDto>();
        CreateMap<ScheduledRouteDoc, ScheduledRouteDto>();
        CreateMap<SearchedRouteDoc, SearchedRouteDto>();
        CreateMap<VehicleDoc, VehicleDto>();
        CreateMap<VehicleRTIDoc, VehicleRTIDto>();
    }

    public MappingProfile()
    {
        MapModels();
        MapCollections();
    }
}
