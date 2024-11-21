using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Seat;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories
{
    public class SeatRepository : BaseRepository<Seat, SeatUpdateCommand>, ISeatRepository
    {
        private readonly AppDbContext _db;

        public SeatRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Seat> QueryById(Guid id)
        {
            return QueryAll().Where(x => x.Id == id);
        }
    }
}
