using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Query.Services.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class UserQueryHandler(
    IMapper mapper,
    IUserService userServiceSoT,
    IUserFavLocationMongoService locationService,
    IUserFavConnectionMongoService connectionService,
    ISearchedRouteMongoService searchedRouteService)
        : IBaseQueryHandler<UserDto>
{
    public async Task<IEnumerable<UserFavLocationDto>> HandleGetFavouriteLocations(Guid userId)
    {
        var locations = await locationService.GetByUserId(userId);
        return mapper.Map<IEnumerable<UserFavLocationDto>>(locations);
    }

    public async Task<IEnumerable<UserFavConnectionDto>> HandleGetFavouriteConnections(Guid userId)
    {
        var connections = await connectionService.GetByUserId(userId);
        return mapper.Map<IEnumerable<UserFavConnectionDto>>(connections);
    }

    public async Task<IEnumerable<SearchedRouteDto>> HandleGetSearchedRoutes(Guid userId)
    {
        var searchedRoutes = await searchedRouteService.GetByUserId(userId);
        return mapper.Map<IEnumerable<SearchedRouteDto>>(searchedRoutes);
    }
    
    public async Task<List<UserDto>> HandleGetAll()
    {
        return await userServiceSoT.GetAllUsers();
    }
}
