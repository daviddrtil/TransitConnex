using Microsoft.EntityFrameworkCore;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Seat;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services;

public class SeatService(ISeatRepository seatRepository, IScheduledRouteRepository scheduledRouteRepository) : ISeatService
{
    public Task<List<SeatDto>> GetAllSeats()
    {
        throw new NotImplementedException();
    }

    public Task<SeatDto> GetSeatById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SeatExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Seat> CreateSeat(SeatCreateCommand createCommand)
    {
        var newSeat = new Seat
        {
            SeatNumber = createCommand.SeatNumber,
            VagonNumber = createCommand.VagonNumber,
            VehicleId = createCommand.VehicleId
        };
        
        await seatRepository.Add(newSeat);
        
        return newSeat;
    }

    public Task<List<Seat>> CreateSeats(List<SeatCreateCommand> createCommands)
    {
        throw new NotImplementedException();
    }

    public async Task ReserveSeats(SeatReservationCommand reservationCommands)
    {
        var scheduledRoute = await scheduledRouteRepository.QueryById(reservationCommands.ScheduledRouteId).FirstOrDefaultAsync();

        if (scheduledRoute == null)
        {
            throw new KeyNotFoundException($"ScheduledRoute with ID: {reservationCommands.ScheduledRouteId} not found.");
        }
        
        var availableSeats = await seatRepository.QueryAvailableSeats(scheduledRoute, reservationCommands.SeatIds);

        if (availableSeats.Count != reservationCommands.SeatIds.Count)
        {
            var notAvailableSeats = reservationCommands.SeatIds.Except(availableSeats.Select(s => s.Id));
            throw new Exception($"Seats with given IDs: {string.Join(", ", notAvailableSeats)} are not available.");
        }

        var reservations = availableSeats.Select(seat => new ScheduledRouteSeat
            {
                SeatId = seat.Id,
                ScheduledRouteId = scheduledRoute.Id,
                ReservedById = reservationCommands.UserId,
                ReservedUntil = DateTime.Now.Add(new TimeSpan(900)),
                IsBought = false
            })
            .ToList();
        
        await seatRepository.AddReservations(reservations);
    }

    public async Task<Seat> EditSeat(SeatUpdateCommand editCommand)
    {
        var seat = await seatRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();

        if (seat == null)
        {
            throw new KeyNotFoundException($"Seat with ID {editCommand.Id} was not found.");
        }
        
        await seatRepository.Update(seat);

        return seat;
    }

    public async Task DeleteSeat(SeatDeleteCommand deleteCommand)
    {
        var seat = await seatRepository.QueryById(deleteCommand.Id).FirstOrDefaultAsync();

        if (seat == null)
        {
            throw new KeyNotFoundException($"Seat with ID {deleteCommand.Id} was not found.");
        }
        
        await seatRepository.Delete(seat);
    }

    public async Task ReserveSeats()
    {
        // TODO -> think if we want to create ScheduledRoute_Seat for each seat in ScheduledRoute or if we want to create it just when reservation is made
        // 1) option when creating ScheduledRoute -> create ScheduledRoute_Seat -> will create a lot records but it will be easier to fetch free seats for given scheduled route -> not good
        // 2) option create ScheduledRoute_Seat only when reservation made -> much less records little worse querying -> will have to 
        
        // TODO -> check if seats are free
        // TODO -> reserve seats for 15 minutes
        throw new NotImplementedException();
    }
}
