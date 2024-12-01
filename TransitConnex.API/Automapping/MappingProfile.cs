using AutoMapper;
using MongoDB.Driver.GeoJsonObjectModel;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Commands.Service;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.DTOs.RouteStop;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;
using Route = TransitConnex.Domain.Models.Route;

namespace TransitConnex.API.Automapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapEnums();
        MapModelsToCommands();
        MapModelsToDTOs();
        MapModelsToCollections();
        MapCollectionsToDTOs();
    }

    public void MapEnums()
    {
        CreateMap<int, LineTypeEnum>().ConvertUsing(src => (LineTypeEnum)src);
        CreateMap<LineTypeEnum, int>().ConvertUsing(src => (int)src);

        CreateMap<int, LocationTypeEnum>().ConvertUsing(src => (LocationTypeEnum)src);
        CreateMap<LocationTypeEnum, int>().ConvertUsing(src => (int)src);

        CreateMap<int, StopTypeEnum>().ConvertUsing(src => (StopTypeEnum)src);
        CreateMap<StopTypeEnum, int>().ConvertUsing(src => (int)src);

        CreateMap<int, VehicleTypeEnum>().ConvertUsing(src => (VehicleTypeEnum)src);
        CreateMap<VehicleTypeEnum, int>().ConvertUsing(src => (int)src);

        // todo
        CreateMap<string, LocationTypeEnum>()
            .ConvertUsing(src => Enum.Parse<LocationTypeEnum>(src, true));
        CreateMap<LocationTypeEnum, string>()
            .ConvertUsing(src => src.ToString());
    }

    private void MapModelsToCommands()
    {
        CreateMap<LocationCreateCommand, Location>();
        CreateMap<IconCreateCommand, Icon>();
        CreateMap<ServiceCreateCommand, Service>();
        CreateMap<StopCreateCommand, Stop>();
        CreateMap<SeatCreateCommand, Seat>();
        CreateMap<RouteCreateCommand, Route>();
        CreateMap<RouteSchedulingTemplateCreateCommand, RouteSchedulingTemplate>();
        CreateMap<StopLocationCommand, LocationStop>();
        CreateMap<UserCreateCommand, User>()
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.Now)); 
        
        
        
        CreateMap<LocationUpdateCommand, Location>();
        CreateMap<IconUpdateCommand, Icon>();
        CreateMap<ServiceUpdateCommand, Service>();
        CreateMap<UserUpdateCommand, User>();
        CreateMap<StopUpdateCommand, Stop>();
        CreateMap<SeatUpdateCommand, Seat>();
        CreateMap<ScheduledRouteUpdateCommand, ScheduledRoute>();
        CreateMap<RouteUpdateCommand, Route>();
        CreateMap<RouteSchedulingTemplateUpdateCommand, RouteSchedulingTemplate>();
        CreateMap<VehicleUpdateCommand, Vehicle>();
    }

    private void MapModelsToDTOs()
    {
        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<ScheduledRoute, ScheduledRouteDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<RouteSchedulingTemplate, RouteSchedulingTemplateDto>();
        CreateMap<User, UserDto>();
        CreateMap<UserLocationFavourite, UserFavLocationDto>().ReverseMap();
        CreateMap<UserConnectionFavourite, UserFavConnectionDto>().ReverseMap();
        CreateMap<Icon, IconDto>();
        CreateMap<Service, ServiceDto>();
        CreateMap<Stop, StopDto>();
    }

    private void MapModelsToCollections()
    {
        CreateMap<Vehicle, VehicleDoc>().ReverseMap();
        CreateMap<UserLocationFavourite, UserFavLocationDoc>().ReverseMap();
        CreateMap<UserConnectionFavourite, UserFavConnectionDoc>().ReverseMap();
        CreateMap<RouteStop, RouteStopDoc>().ReverseMap();
        CreateMap<ScheduledRoute, ScheduledRouteDoc>()
            .ForMember(dest => dest.HasTickets, opt =>
                opt.MapFrom(src => src.Route != null && src.Route.HasTickets))
            .ForMember(dest => dest.Stops,
                opt => opt.MapFrom(src => src.Route != null && src.Route.Stops != null
                    ? src.Route.Stops.OrderBy(rs => rs.StopOrder)
                        .Select(rs => new RouteStopDoc
                        {
                            Id = rs.StopId,
                            Name = rs.Stop != null ? rs.Stop.Name : null,
                            DepartureTime = src.StartTime.Add(rs.TimeDurationFromFirstStop),
                            Order = rs.StopOrder
                        }).ToList()
                    : new List<RouteStopDoc>()));

        // Map location coordinates
        CreateMap<Location, LocationDoc>()
            .ForMember(dest => dest.Coordinates,
                opt => opt.MapFrom(src => new GeoJsonPoint<GeoJson2DCoordinates>(
                    new GeoJson2DCoordinates(src.Longitude ?? 0, src.Latitude ?? 0))))
            .ForMember(dest => dest.Stops,
                opt => opt.MapFrom(src => src.Stops.Select(s => s.Id)));
        CreateMap<LocationDoc, Location>()
            .ForMember(dest => dest.Longitude,
                opt => opt.MapFrom(src => src.Coordinates.Coordinates.X))
            .ForMember(dest => dest.Latitude,
                opt => opt.MapFrom(src => src.Coordinates.Coordinates.Y));
    }

    private void MapCollectionsToDTOs()
    {
        CreateMap<UserFavLocationDoc, UserFavLocationDto>().ReverseMap();
        CreateMap<UserFavConnectionDoc, UserFavConnectionDto>().ReverseMap();
        CreateMap<ScheduledRouteDoc, ScheduledRouteDto>().ReverseMap();
        CreateMap<SearchedRouteDoc, SearchedRouteDto>().ReverseMap();
        CreateMap<VehicleDoc, VehicleDto>().ReverseMap();
        CreateMap<VehicleRTIDoc, VehicleRTIDto>().ReverseMap();
        CreateMap<RouteStopDoc, RouteStopDto>().ReverseMap();

        // Map location coordinates
        CreateMap<LocationDto, LocationDoc>()
            .ForMember(dest => dest.Coordinates,
                opt => opt.MapFrom(src => new GeoJsonPoint<GeoJson2DCoordinates>(
                    new GeoJson2DCoordinates(src.Longitude ?? 0, src.Latitude ?? 0))));
        CreateMap<LocationDoc, LocationDto>()
            .ForMember(dest => dest.Longitude,
                opt => opt.MapFrom(src => src.Coordinates.Coordinates.X))
            .ForMember(dest => dest.Latitude,
                opt => opt.MapFrom(src => src.Coordinates.Coordinates.Y));
    }
}
