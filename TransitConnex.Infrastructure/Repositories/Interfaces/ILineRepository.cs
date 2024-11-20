using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface ILineRepository : IBaseRepository<Line>
    {
        IQueryable<Line> QueryById(Guid id);
    }
}
