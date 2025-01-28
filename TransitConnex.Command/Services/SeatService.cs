using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class SeatService(IMapper mapper, ISeatRepository seatRepository, IScheduledRouteRepository scheduledRouteRepository, IVehicleRepository vehicleRepository, IRouteTicketRepository routeTicketRepository, IUserRepository userRepository) : ISeatService
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
            bool reserved = !availableSeats.Any(x => x.Id == seat.Id);
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

    public async Task<List<SeatDto>> GetSeatsFiltered(SeatFilteredQuery filter)
    {
        var query = seatRepository.QueryAll();

        if (filter.VehicleId != null)
        {
            query = query.Where(x => x.VehicleId == filter.VehicleId);
        }

        var seats = await query.ToListAsync();
        
        return mapper.Map<List<SeatDto>>(seats);
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
        var newSeats = mapper.Map<List<Seat>>(createCommands);
        await seatRepository.AddBatch(newSeats);
        
        return newSeats;
    }
    
    public async Task ReserveSeats(SeatReservationCommand reservationCommands)
    {
        if (!await scheduledRouteRepository.Exists(reservationCommands.ScheduledRouteId))
        {
            throw new KeyNotFoundException($"ScheduledRoute with ID: {reservationCommands.ScheduledRouteId} not found."); 
        }
        
        var availableSeats = await seatRepository.QueryAvailableSeats(reservationCommands.ScheduledRouteId, reservationCommands.SeatIds);

        if (availableSeats.Count != reservationCommands.SeatIds.Count)
        {
            var notAvailableSeats = reservationCommands.SeatIds.Except(availableSeats.Select(s => s.Id));
            throw new Exception($"Seats with given IDs: {string.Join(", ", notAvailableSeats)} are not available.");
        }
        
        var user = await userRepository.QueryById(reservationCommands.UserId).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID: {reservationCommands.UserId} does not exist.");
        }
        
        var reservations = availableSeats.Select(seat => new ScheduledRouteSeat
        {
            SeatId = seat.Id,
            ScheduledRouteId = reservationCommands.ScheduledRouteId,
            ReservedById = user.Id,
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

        var reservations = await seatRepository.QuerySeatReservations(seat.Id);
        var routeTicketsIds = reservations.Where(x => x.ReservedById != null).Select(r => r.RouteTicketId).ToList();
        await scheduledRouteRepository.DeleteReservations(reservations);
        
        var routeTickets = await routeTicketRepository.QueryAll().Where(x => routeTicketsIds.Contains(x.Id)).ToListAsync();
        await routeTicketRepository.DeleteBatch(routeTickets);
        
        await seatRepository.Delete(seat);
    }
}
