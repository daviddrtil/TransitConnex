using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class UserQueryHandler(IMapper mapper, IUserService userServiceSoT) : IBaseQueryHandler<UserDto>
{
    public async Task<List<UserDto>> HandleGetAll()
    {
        return await userServiceSoT.GetAllUsers();
    }
}
