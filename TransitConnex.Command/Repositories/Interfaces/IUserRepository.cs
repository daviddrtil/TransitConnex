using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IQueryable<User> QueryById(Guid id);
    }
}
