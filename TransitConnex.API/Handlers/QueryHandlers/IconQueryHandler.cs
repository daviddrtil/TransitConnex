using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class IconQueryHandler(IIconService iconServiceSoT) : IBaseQueryHandler<IconDto>
{
    public async Task<List<IconDto>> HandleGetFiltered(IconFilteredQuery filter)
    {
        return await iconServiceSoT.GetFilteredIcons(filter);
    }
}
