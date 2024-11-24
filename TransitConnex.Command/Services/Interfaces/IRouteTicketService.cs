using TransitConnex.Domain.DTOs.RouteTicket;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteTicketService
{
    Task<List<RouteTicketDto>> GetAllRouteTickets();

    Task<RouteTicketDto> GetRouteTicketById(Guid id);

    Task<bool> RouteTicketExists(Guid id);

    Task<RouteTicketDto> CreateRouteTicket(RouteTicketCreateDto routeTicketDto);

    Task<RouteTicketDto> EditRouteTicket(Guid id, RouteTicketCreateDto editedRouteTicket);

    Task DeleteRouteTicket(Guid id);
}
