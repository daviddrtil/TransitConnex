using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface IScheduledRouteMongoService
{
    Task<IEnumerable<ScheduledRoute>> GetAll();
    Task<ScheduledRoute?> GetById(Guid id);
    Task<Guid> Create(ScheduledRoute scheduledRoute);
    Task Update(ScheduledRoute scheduledRoute);
    Task Delete(Guid id);

    Task<IEnumerable<Guid>> Create(IEnumerable<ScheduledRoute> scheduledRoutes);
    Task Delete(IEnumerable<Guid> ids);
}
