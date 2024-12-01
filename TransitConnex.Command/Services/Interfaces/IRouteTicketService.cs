using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteTicketService
{
    Task<List<RouteTicketDto>> GetRouteTicketsFiltered();
    Task<RouteTicketDto> GetRouteTicketById(Guid id);
    

    Task<RouteTicket> CreateRouteTicket(RouteTicketCreateCommand createCommand);
    Task DeleteRouteTicket(Guid id);
}
