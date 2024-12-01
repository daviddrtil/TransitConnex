using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.RouteTicket;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteTicket;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Command.Services;

public class RouteTicketService(IMapper mapper, IRouteTicketRepository routeTicketRepository, IScheduledRouteRepository scheduledRouteRepository, ISeatRepository seatRepository) : IRouteTicketService
{
    public async Task<List<RouteTicketDto>> GetRouteTicketsFiltered(RouteTicketFilteredQuery filter)
    {
        var query = routeTicketRepository.QueryAll();

        if (filter.RouteId != null)
        {
            query = query.Where(r => r.ScheduledRouteId == filter.RouteId);
        }

        if (filter.UserId != null)
        {
            query = query.Where(r => r.UserId == filter.UserId);
        }
        
        var routeTickets = await query.ToListAsync();
        return mapper.Map<List<RouteTicketDto>>(routeTickets);
    }

    public async Task<RouteTicket> CreateRouteTicket(RouteTicketCreateCommand createCommand)
    {
        var scheduledRoute = await scheduledRouteRepository.QueryById(createCommand.ScheduledRouteId).FirstOrDefaultAsync();
        if (scheduledRoute == null)
        {
            throw new KeyNotFoundException($"Scheduled route with id: {createCommand.ScheduledRouteId} does not exist.");
        }
        
        var seatReservations = await seatRepository.QuerySeatReservationsForScheduled(scheduledRoute.Id, createCommand.SeatIds, createCommand.UserId, false);
        if (seatReservations == null || seatReservations.Count == 0 ||
            seatReservations.Count() != createCommand.SeatIds.Count())
        {
            throw new ArgumentException("Not all seats reservations were created.");
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
        
        await scheduledRouteRepository.DeleteReservations(reservations);
        await routeTicketRepository.Delete(routeTicket);
    }
}
