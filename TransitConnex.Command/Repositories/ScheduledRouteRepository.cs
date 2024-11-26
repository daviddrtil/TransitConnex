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
    public IQueryable<ScheduledRoute> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    // public new async Task<bool> Exists(Guid id)
    // {
    //     return await db.ScheduledRoutes.AnyAsync(x => x.Id == id);
    // }
}
