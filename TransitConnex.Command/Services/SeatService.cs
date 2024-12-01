using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class SeatService(IMapper mapper, ISeatRepository seatRepository, IScheduledRouteRepository scheduledRouteRepository, IVehicleRepository vehicleRepository, IRouteTicketRepository routeTicketRepository) : ISeatService
{
    public async Task<List<SeatDto>> GetSeatsForScheduledRoute(Guid scheduledRouteId)
    {
        var scheduledRoute = await scheduledRouteRepository.QueryById(scheduledRouteId).FirstOrDefaultAsync();
        if (scheduledRoute == null)
        {
            throw new KeyNotFoundException($"Scheduled route with ID: {scheduledRouteId} does not exist");
        }
        
        var allSeats = await seatRepository.QueryAll().Where(x => x.VehicleId == scheduledRoute.VehicleId).ToListAsync();
        var availableSeats = await seatRepository.QueryAvailableSeats(scheduledRouteId, []);

        var seats = new List<SeatDto>();
        foreach (var seat in allSeats)
        {
            bool reserved = !availableSeats.Contains(seat);
            var seatDto = new SeatDto
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
                Reserved = reserved,
                VagonNumber = seat.VagonNumber ?? 0,
                VehicleId = seat.VehicleId,
            };
            seats.Add(seatDto);
        }
        
        return seats;
    }

    public async Task<Seat> CreateSeat(SeatCreateCommand createCommand)
    {
        if (!await vehicleRepository.Exists(createCommand.VehicleId))
        {
            throw new KeyNotFoundException($"Seats vehicle with id: {createCommand.VehicleId} does not exist");
        }
        
        var newSeat = mapper.Map<Seat>(createCommand);
        await seatRepository.Add(newSeat);

        return newSeat;
    }

    public async Task<List<Seat>> CreateSeats(List<SeatCreateCommand> createCommands)
    {
        // TODO vehicle validations
        // if(createCommands.Any(async x => !await vehicleRepository.Exists(x.VehicleId)))
        
        var newSeats = mapper.Map<List<Seat>>(createCommands);
        await seatRepository.AddBatch(newSeats);
        
        return newSeats;
    }

    // TODO -> Init reserve will try to reserve all seats given from FE (for count of tickets to buy)
    // Other reserves will always contain only one seat -> picking other seats
        // user fake unlocks reserved seat, picks other, we try to reserve if success we free fake unlocked
        // but that is FE logic so we dont care
    public async Task ReserveSeats(SeatReservationCommand reservationCommands) // TODO -> add check if seats are valid for scheduledroute
    {
        if (!await scheduledRouteRepository.Exists(reservationCommands.ScheduledRouteId))
        {
            throw new KeyNotFoundException($"ScheduledRoute with ID: {reservationCommands.ScheduledRouteId} not found."); 
        }

        // TODO -> sent timeLeft from FE? for reservation
        var availableSeats = await seatRepository.QueryAvailableSeats(reservationCommands.ScheduledRouteId, reservationCommands.SeatIds);

        if (availableSeats.Count != reservationCommands.SeatIds.Count)
        {
            var notAvailableSeats = reservationCommands.SeatIds.Except(availableSeats.Select(s => s.Id));
            throw new Exception($"Seats with given IDs: {string.Join(", ", notAvailableSeats)} are not available.");
        }
        
        var reservations = availableSeats.Select(seat => new ScheduledRouteSeat
        {
            SeatId = seat.Id,
            ScheduledRouteId = reservationCommands.ScheduledRouteId,
            ReservedById = reservationCommands.UserId,
            ReservedUntil = DateTime.Now.AddMinutes(15),
            RouteTicket = null
        })
            .ToList();

        await seatRepository.UpsertReservations(reservations);
    }

    public async Task FreeSeats(SeatReservationCommand reservationCommands)
    {
        if (!await scheduledRouteRepository.Exists(reservationCommands.ScheduledRouteId))
        {
            throw new KeyNotFoundException($"ScheduledRoute with ID: {reservationCommands.ScheduledRouteId} not found.");
        }
        
        var reservations = await seatRepository.QuerySeatReservationsForScheduled(reservationCommands.ScheduledRouteId, reservationCommands.SeatIds, reservationCommands.UserId);
        foreach (var reservation in reservations)
        {
            reservation.ReservedUntil = DateTime.Now;
            reservation.ReservedById = null;
        }
        
        await seatRepository.UpsertReservations(reservations);
    }

    public async Task<Seat> EditSeat(SeatUpdateCommand editCommand)
    {
        var seat = await seatRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();

        if (seat == null)
        {
            throw new KeyNotFoundException($"Seat with ID {editCommand.Id} was not found.");
        }

        seat = mapper.Map(editCommand, seat);
        await seatRepository.Update(seat);

        return seat;
    }

    public async Task DeleteSeat(Guid id)
    {
        var seat = await seatRepository.QueryById(id).FirstOrDefaultAsync();
        if (seat == null)
        {
            throw new KeyNotFoundException($"Seat with ID {id} was not found.");
        }

        // TODO -> improvement -> rn very simple -> deletes all reservations + tickets -> for future think of betteer logic -> rereservation
        var reservations = await seatRepository.QuerySeatReservations(seat.Id);
        var routeTicketsIds = reservations.Where(x => x.ReservedById != null).Select(r => r.RouteTicketId).ToList();
        await scheduledRouteRepository.DeleteReservations(reservations);
        
        var routeTickets = await routeTicketRepository.QueryAll().Where(x => routeTicketsIds.Contains(x.Id)).ToListAsync();
        await routeTicketRepository.DeleteBatch(routeTickets);
        
        await seatRepository.Delete(seat);
    }
}
