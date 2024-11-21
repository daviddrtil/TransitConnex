using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Line;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
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
}
