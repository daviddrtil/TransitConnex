using TransitConnex.Command.Commands.Seat;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface ISeatService
{
    Task<List<SeatDto>> GetSeatsForScheduledRoute(Guid scheduledRouteId);
    Task<List<SeatDto>> GetSeatsFiltered(SeatFilteredQuery filter);

    Task<Seat> CreateSeat(SeatCreateCommand createCommand);
    Task<List<Seat>> CreateSeats(List<SeatCreateCommand> createCommands);
    Task ReserveSeats(SeatReservationCommand reservationCommands);
    Task FreeSeats(SeatReservationCommand reservationCommands);
    Task<Seat> EditSeat(SeatUpdateCommand editCommand);
    Task DeleteSeat(Guid id);
}
