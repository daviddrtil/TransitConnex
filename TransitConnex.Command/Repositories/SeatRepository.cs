using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class SeatRepository(AppDbContext db) : BaseRepository<Seat, SeatUpdateCommand>(db), ISeatRepository
{
    private readonly AppDbContext Db = db;

    public IQueryable<Seat> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public async Task<List<Seat>> QueryAvailableSeats(Guid scheduledRouteId, List<Guid>? SeatIds)
    {
        var takenSeatIds = Db.ScheduledRouteSeats.Where(x => x.ScheduledRouteId == scheduledRouteId && x.ReservedUntil >= DateTime.Now)
            .Select(x => x.SeatId)
            .ToList();

        var query = QueryAll().Where(x => !takenSeatIds.Contains(x.Id));

        if (SeatIds != null && SeatIds.Any())
        {
            query = query.Where(x => SeatIds.Contains(x.Id));
        }

        return await query.ToListAsync();
    }

    public async Task<List<ScheduledRouteSeat>> QuerySeatReservations(Guid seatId)
    {
        var query = Db.ScheduledRouteSeats.Where(x => x.SeatId == seatId);
        
        return await query.ToListAsync();
    }

    public async Task<List<ScheduledRouteSeat>> QuerySeatReservationsForScheduled(Guid scheduledRouteId,
        List<Guid>? SeatIds,
        Guid? UserId, bool QueryBought = true)
    {
        var query = Db.ScheduledRouteSeats.Where(x => x.ScheduledRouteId == scheduledRouteId);

        if (UserId != null)
        {
            query = query.Where(x => x.ReservedById == UserId);
        }

        if (SeatIds != null && SeatIds.Any())
        {
            query = query.Where(x => SeatIds.Contains(x.SeatId));
        }

        if (!QueryBought)
        {
            query = query.Where(x => x.RouteTicket == null);
        }
        
        return await query.ToListAsync();
    }

    public async Task<List<ScheduledRouteSeat>> QuerySeatReservationsForTicket(Guid routeTicketId)
    {
        var query = Db.ScheduledRouteSeats.Where(x => x.RouteTicketId == routeTicketId);
        
        return await query.ToListAsync();
    }

    public async Task AddReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        Db.ScheduledRouteSeats.AddRange(scheduledRouteSeats);
        await Db.SaveChangesAsync();
    }

    public async Task UpsertReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        await Db.BulkInsertOrUpdateAsync(scheduledRouteSeats);
    }

    public async Task UpdateReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        Db.ScheduledRouteSeats.UpdateRange(scheduledRouteSeats);
        await Db.SaveChangesAsync();
    }
}
