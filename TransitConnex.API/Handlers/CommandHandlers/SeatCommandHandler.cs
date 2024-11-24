using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class SeatCommandHandler(ISeatService seatService) : IBaseCommandHandler<ISeatCommand>
{
    public async Task<Guid> HandleCreate(ISeatCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleUpdate(ISeatCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleDelete(ISeatCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleSeatReservation(ISeatCommand command)
    {
        if (command is not SeatReservationCommand reservationCommand)
        {
            throw new InvalidCastException("Invalid command given, expected SeatReservationCommand.");
        }

        await seatService.ReserveSeats(reservationCommand);
    }
}
