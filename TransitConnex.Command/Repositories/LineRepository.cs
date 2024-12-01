using TransitConnex.Command.Commands.Line;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class LineRepository(AppDbContext db) : BaseRepository<Line, LineUpdateCommand>(db), ILineRepository
{
    public IQueryable<Line> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
