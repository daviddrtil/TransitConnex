using EFCore.BulkExtensions;
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

    public async Task UpsertBatch(List<ScheduledRoute> scheduledRoutes)
    {
        await db.BulkInsertOrUpdateAsync(scheduledRoutes, options =>
            {
                options.UpdateByProperties = new List<string>
                {
                    nameof(ScheduledRoute.StartTime), nameof(ScheduledRoute.RouteId)
                };
                options.SetOutputIdentity = true;
            }
        );
    }
}
