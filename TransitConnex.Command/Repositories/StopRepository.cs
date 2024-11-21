using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Stop;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop, StopUpdateCommand>, IStopRepository
    {
        private readonly AppDbContext _db;

        public StopRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Stop> QueryById(Guid id)
        {
            return QueryAll().Where(x => x.Id == id);
        }
    }
}
