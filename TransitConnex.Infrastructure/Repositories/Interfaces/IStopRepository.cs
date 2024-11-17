using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IStopRepository : IBaseRepository<Stop>
    {
        IQueryable<Stop> QueryById(Guid id);
    }
}
