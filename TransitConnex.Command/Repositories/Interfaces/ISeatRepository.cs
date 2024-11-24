using TransitConnex.Command.Commands.Seat;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface ISeatRepository : IBaseRepository<Seat, SeatUpdateCommand>
{
    IQueryable<Seat> QueryById(Guid id);

    Task<List<Seat>> QueryAvailableSeats(ScheduledRoute scheduledRoute, List<Guid>? SeatIds);

    Task AddReservations(List<ScheduledRouteSeat> scheduledRouteSeats);
}
