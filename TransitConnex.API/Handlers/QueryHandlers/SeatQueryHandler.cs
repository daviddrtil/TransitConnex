using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class SeatQueryHandler(ISeatService seatService) : IBaseQueryHandler<SeatDto>
{
    public async Task<List<SeatDto>> HandleGetSeatsForScheduledRouteWithState(Guid scheduledRouteId)
    {
        return await seatService.GetSeatsForScheduledRoute(scheduledRouteId);
    }
    
    public async Task<List<SeatDto>> HandleGetSeatsFiltered(SeatFilteredQuery filter)
    {
        return await seatService.GetSeatsFiltered(filter);
    }
}
