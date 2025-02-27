using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class ScheduledRouteRepository(AppDbContext db)
    : BaseRepository<ScheduledRoute, ScheduledRouteUpdateCommand>(db),
        IScheduledRouteRepository
{
    private readonly AppDbContext Db = db;

    private IQueryable<ScheduledRoute> QueryScheduledRoutes()
    {
        return QueryAll()
            .Include(sr => sr.Route)
            .ThenInclude(r => r!.Stops
                .OrderBy(rs => rs.StopOrder))
            .ThenInclude(rs => rs.Stop);
    }

    public IQueryable<ScheduledRoute> QueryById(Guid id) 
    {
        return QueryScheduledRoutes()
            .Where(x => x.Id == id);
    }

    public IQueryable<ScheduledRoute> QueryAllScheduledRoutes()
    {
        return QueryScheduledRoutes();
    }

    public async Task<List<ScheduledRouteSeat>> GetReservationsForScheduledRoute(Guid scheduledRouteId)
    {
       return await Db.ScheduledRouteSeats.Where(x => x.ScheduledRouteId == scheduledRouteId).ToListAsync();
    }

    public async Task UpsertBatch(List<ScheduledRoute> scheduledRoutes)
    {
        // await db.BulkInsertOrUpdateAsync(scheduledRoutes, options =>
        //     {
        //         options.UpdateByProperties = new List<string>
        //         {
        //             nameof(ScheduledRoute.StartTime), nameof(ScheduledRoute.RouteId)
        //         };
        //         options.SetOutputIdentity = true;
        //     }
        // );
        
        await Db.BulkInsertOrUpdateAsync(scheduledRoutes);
    }
    
    public async Task DeleteReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        Db.ScheduledRouteSeats.RemoveRange(scheduledRouteSeats);
        await Db.SaveChangesAsync();
    }
}
