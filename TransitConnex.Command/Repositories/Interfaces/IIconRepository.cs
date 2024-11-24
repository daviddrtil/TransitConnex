using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Icon;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface IIconRepository : IBaseRepository<Icon, IconUpdateCommand>
{
    IQueryable<Icon> QueryById(Guid id);

    IQueryable<Icon> QueryByName(string name);
}
