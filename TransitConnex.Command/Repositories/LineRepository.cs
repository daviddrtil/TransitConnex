using TransitConnex.Command.Commands.Line;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class LineRepository : BaseRepository<Line, LineUpdateCommand>, ILineRepository
{
    private readonly AppDbContext _db;

    public LineRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Line> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
