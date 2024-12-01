using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteTicketService
{
    Task<List<RouteTicketDto>> GetRouteTicketsFiltered(RouteTicketFilteredQuery filter);

    Task<RouteTicket> CreateRouteTicket(RouteTicketCreateCommand createCommand);
    Task DeleteRouteTicket(Guid id);
}
