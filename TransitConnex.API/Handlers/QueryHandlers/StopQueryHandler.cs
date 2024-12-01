using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class StopQueryHandler(IStopService stopServiceSoT) : IBaseQueryHandler<StopDto>
{
    public async Task<List<StopDto>> HandleGetFiltered(StopFilteredQuery filter)
    {
        return await stopServiceSoT.GetFilteredStops(filter);
    }
}
