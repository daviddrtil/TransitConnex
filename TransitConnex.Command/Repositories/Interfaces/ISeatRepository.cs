using TransitConnex.Command.Commands.Seat;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface ISeatRepository : IBaseRepository<Seat, SeatUpdateCommand>
{
    IQueryable<Seat> QueryById(Guid id);

    Task<List<Seat>> QueryAvailableSeats(Guid scheduledRouteId, List<Guid>? SeatIds);
    Task<List<ScheduledRouteSeat>> QuerySeatReservations(Guid seatId);
    Task<List<ScheduledRouteSeat>> QuerySeatReservationsForScheduled(Guid scheduledRouteId, List<Guid>? SeatIds,
        Guid? UserId,
        bool QueryBought = true);

    Task<List<ScheduledRouteSeat>> QuerySeatReservationsForTicket(Guid routeTicketId);

    Task AddReservations(List<ScheduledRouteSeat> scheduledRouteSeats);

    Task UpsertReservations(List<ScheduledRouteSeat> scheduledRouteSeats);

    Task UpdateReservations(List<ScheduledRouteSeat> scheduledRouteSeats);
}
