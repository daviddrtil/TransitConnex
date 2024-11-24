using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Line;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface ILineRepository : IBaseRepository<Line, LineUpdateCommand>
{
    IQueryable<Line> QueryById(Guid id);
}
