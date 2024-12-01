using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class RouteTicketQueryHandler(IRouteTicketService routeTicketServiceSoT) : IBaseQueryHandler<RouteTicketDto>
{
    public async Task<List<RouteTicketDto>> HandleGetFiltered(RouteTicketFilteredQuery filter)
    {
        return await routeTicketServiceSoT.GetRouteTicketsFiltered(filter);
    }
}
