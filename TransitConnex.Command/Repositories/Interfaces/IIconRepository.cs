using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IIconRepository : IBaseRepository<Icon>
    {
        IQueryable<Icon> QueryById(Guid id);

        IQueryable<Icon> QueryByName(string name);
    }
}
