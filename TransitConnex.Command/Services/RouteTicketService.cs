using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class RouteTicketService(IRouteTicketRepository routeTicketRepository, IScheduledRouteRepository scheduledRouteRepository, ISeatRepository seatRepository) : IRouteTicketService
{
    public Task<List<RouteTicketDto>> GetRouteTicketsFiltered()
    {
        throw new NotImplementedException();
    }

    public Task<RouteTicketDto> GetRouteTicketById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<RouteTicket> CreateRouteTicket(RouteTicketCreateCommand createCommand)
    {
        // TODO -> should check if seats are reserved for given user -> if not -> handle
        // TODO -> get scheduled route and check existence
        // same for user?
        var scheduledRoute = await scheduledRouteRepository.QueryById(createCommand.ScheduledRouteId).FirstOrDefaultAsync();
        if (scheduledRoute == null)
        {
            throw new KeyNotFoundException($"Scheduled route with id: {createCommand.ScheduledRouteId} does not exist.");
        }
        
        var seatReservations = await seatRepository.QuerySeatReservations(scheduledRoute.Id, createCommand.SeatIds, createCommand.UserId, false);
        if (seatReservations == null || seatReservations.Count == 0 ||
            seatReservations.Count() != createCommand.SeatIds.Count())
        {
            throw new ArgumentException("Not all seats reservations were created."); // TODO -> improve message
        }
        
        var newRouteTicket = new RouteTicket()
        {
            Price = createCommand.Price,
            UserId = createCommand.UserId,
            ScheduledRouteId = createCommand.ScheduledRouteId,
            ValidFrom = scheduledRoute.StartTime,
            ValidTo = scheduledRoute.EndTime,
        };
        
        await routeTicketRepository.Add(newRouteTicket);

        foreach (var seatReservation in seatReservations)
        {
            seatReservation.RouteTicketId = newRouteTicket.Id;
            seatReservation.ReservedUntil = scheduledRoute.EndTime;
        }
        
        await seatRepository.UpdateReservations(seatReservations);

        return newRouteTicket;
    }

    public Task<RouteTicket> EditRouteTicket(RouteTicketUpdateCommand editCommand)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRouteTicket(Guid id)
    {
        var routeTicket = await routeTicketRepository.QueryById(id).FirstOrDefaultAsync();
        if (routeTicket == null)
        {
            throw new KeyNotFoundException($"Route ticket with id: {id} does not exist.");
        }
        
        var reservations = await seatRepository.QuerySeatReservationsForTicket(routeTicket.Id);
        
        await seatRepository.DeleteReservations(reservations);
        
        await routeTicketRepository.Delete(routeTicket);
    }

    public Task DeleteRouteTickets(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    private Task ValidateCreateCommand(ScheduledRoute scheduledRoute, User user)
    {
        throw new NotImplementedException(); // TODO
    }
}
