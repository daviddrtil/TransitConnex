using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class SeatCommandHandler(ISeatService seatService) : IBaseCommandHandler<ISeatCommand>
{
    public async Task<Guid> HandleCreate(ISeatCommand command)
    {
        if (command is not SeatCreateCommand createCommand)
        {
            throw new InvalidCastException();
        }
        
        var seat = await seatService.CreateSeat(createCommand);
        return seat.Id;
    }

    public async Task HandleUpdate(ISeatCommand command)
    {
        if (command is not SeatUpdateCommand editCommand)
        {
            throw new InvalidCastException();
        }
        
        var seat = await seatService.EditSeat(editCommand);
    }

    public async Task HandleDelete(Guid id)
    {
        await seatService.DeleteSeat(id);
    }

    public async Task HandleSeatReservation(ISeatCommand command)
    {
        if (command is not SeatReservationCommand reservationCommand)
        {
            throw new InvalidCastException("Invalid command given, expected SeatReservationCommand.");
        }

        await seatService.ReserveSeats(reservationCommand);
    }
    
    public async Task HandleSeatFree(ISeatCommand command)
    {
        if (command is not SeatReservationCommand reservationCommand)
        {
            throw new InvalidCastException("Invalid command given, expected SeatReservationCommand.");
        }

        await seatService.FreeSeats(reservationCommand);
    }
}
