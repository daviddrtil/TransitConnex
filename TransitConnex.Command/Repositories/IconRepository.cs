using TransitConnex.Command.Commands.Icon;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

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
