using TransitConnex.Command.Commands.Line;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface ILineRepository : IBaseRepository<Line, LineUpdateCommand>
{
    IQueryable<Line> QueryById(Guid id);
}
