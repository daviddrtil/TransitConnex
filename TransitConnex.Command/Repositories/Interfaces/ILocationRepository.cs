using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        IQueryable<Location> QueryById(Guid id);
    }
}
