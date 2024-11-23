using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface IScheduledRouteMongoService
{
    Task<IEnumerable<ScheduledRoute>> GetAll();
    Task<ScheduledRoute?> GetById(Guid id);
    Task<Guid> Create(ScheduledRoute scheduledRoute);
    Task Update(ScheduledRoute scheduledRoute);
    Task Delete(Guid id);
}
