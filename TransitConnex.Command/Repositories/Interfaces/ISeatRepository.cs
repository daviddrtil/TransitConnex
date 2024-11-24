using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Seat;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface ISeatRepository : IBaseRepository<Seat, SeatUpdateCommand>
{
    IQueryable<Seat> QueryById(Guid id);

    Task<List<Seat>> QueryAvailableSeats(ScheduledRoute scheduledRoute, List<Guid>? SeatIds);

    Task AddReservations(List<ScheduledRouteSeat> scheduledRouteSeats);
}
