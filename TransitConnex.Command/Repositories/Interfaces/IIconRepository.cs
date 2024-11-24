using TransitConnex.Command.Commands.Icon;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IIconRepository : IBaseRepository<Icon, IconUpdateCommand>
{
    IQueryable<Icon> QueryById(Guid id);

    IQueryable<Icon> QueryByName(string name);
}
