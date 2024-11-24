using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Icon;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories;

public class IconRepository : BaseRepository<Icon, IconUpdateCommand>, IIconRepository
{
    private readonly AppDbContext _db;

    public IconRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Icon> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public IQueryable<Icon> QueryByName(string name)
    {
        return QueryAll().Where(x => x.Name == name);
    }
}
