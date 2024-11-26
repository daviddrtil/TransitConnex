using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteTicketService
{
    Task<List<RouteTicketDto>> GetAllRouteTickets();

    Task<RouteTicketDto> GetRouteTicketById(Guid id);

    Task<bool> RouteTicketExists(Guid id);

    Task<RouteTicket> CreateRouteTicket(RouteTicketCreateCommand createCommand);

    Task<RouteTicket> EditRouteTicket(RouteTicketUpdateCommand editCommand);

    Task DeleteRouteTicket(Guid id);
}
