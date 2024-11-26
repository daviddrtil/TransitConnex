using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class SeatRepository : BaseRepository<Seat, SeatUpdateCommand>, ISeatRepository
{
    private readonly AppDbContext _db;

    public SeatRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Seat> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public async Task<List<Seat>> QueryAvailableSeats(Guid scheduledRouteId, List<Guid>? SeatIds)
    {
        var takenSeatIds = _db.ScheduledRouteSeats.Where(x => x.ScheduledRouteId == scheduledRouteId && x.ReservedUntil >= DateTime.Now)
            .Select(x => x.SeatId)
            .ToList();

        var query = QueryAll().Where(x => !takenSeatIds.Contains(x.Id));

        if (SeatIds != null && SeatIds.Any())
        {
            query = query.Where(x => SeatIds.Contains(x.Id));
        }

        return await query.ToListAsync();
    }

    public async Task<List<ScheduledRouteSeat>> QuerySeatReservations(Guid scheduledRouteId, List<Guid>? SeatIds,
        Guid? UserId, bool QueryBought = true)
    {
        var query = _db.ScheduledRouteSeats.Where(x => x.ScheduledRouteId == scheduledRouteId);

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
        var query = _db.ScheduledRouteSeats.Where(x => x.RouteTicketId == routeTicketId);
        
        return await query.ToListAsync();
    }

    public async Task AddReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        _db.ScheduledRouteSeats.AddRange(scheduledRouteSeats);
        await _db.SaveChangesAsync();
    }

    public async Task UpsertReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        await _db.BulkInsertOrUpdateAsync(scheduledRouteSeats);
    }

    public async Task UpdateReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        _db.ScheduledRouteSeats.UpdateRange(scheduledRouteSeats);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        _db.ScheduledRouteSeats.RemoveRange(scheduledRouteSeats);
        await _db.SaveChangesAsync();
    }
}
