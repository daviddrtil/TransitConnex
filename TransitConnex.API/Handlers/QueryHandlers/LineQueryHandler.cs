using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Line;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class LineQueryHandler(ILineService lineServiceSoT) : IBaseQueryHandler<LineDto>
{
    public async Task<List<LineDto>> HandleGetFiltered(LineFilteredQuery filter)
    {
        return await lineServiceSoT.GetLinesFiltered(filter);
    }
}
