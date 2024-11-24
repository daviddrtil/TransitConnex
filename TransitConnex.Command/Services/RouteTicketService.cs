using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteTicket;

namespace TransitConnex.Command.Services;

public class RouteTicketService(IRouteTicketRepository routeTicketRepository) : IRouteTicketService
{
    public Task<List<RouteTicketDto>> GetAllRouteTickets()
    {
        throw new NotImplementedException();
    }

    public Task<RouteTicketDto> GetRouteTicketById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RouteTicketExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RouteTicketDto> CreateRouteTicket(RouteTicketCreateDto routeTicketDto)
    {
        throw new NotImplementedException();
    }

    public Task<RouteTicketDto> EditRouteTicket(Guid id, RouteTicketCreateDto editedRouteTicket)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRouteTicket(Guid id)
    {
        throw new NotImplementedException();
    }
}
