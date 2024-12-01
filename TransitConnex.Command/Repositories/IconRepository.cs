using TransitConnex.Command.Commands.Icon;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class IconRepository(AppDbContext db) : BaseRepository<Icon, IconUpdateCommand>(db), IIconRepository
{
    public IQueryable<Icon> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public IQueryable<Icon> QueryByName(string name)
    {
        return QueryAll().Where(x => x.Name == name);
    }
}
