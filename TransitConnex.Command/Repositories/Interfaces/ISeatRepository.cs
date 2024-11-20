using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface ISeatRepository : IBaseRepository<Seat>
    {
        IQueryable<Seat> QueryById(Guid id);
    }
}
