using TransitConnex.Command.Commands.Seat;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface ISeatService
{
    Task<List<SeatDto>> GetAllSeats();

    Task<SeatDto> GetSeatById(Guid id);

    Task<bool> SeatExists(Guid id);

    Task<Seat> CreateSeat(SeatCreateCommand createCommand);

    Task<List<Seat>> CreateSeats(List<SeatCreateCommand> createCommands);

    Task ReserveSeats(SeatReservationCommand reservationCommands);

    Task<Seat> EditSeat(SeatUpdateCommand editCommand);

    Task DeleteSeat(SeatDeleteCommand deleteCommand);
}
