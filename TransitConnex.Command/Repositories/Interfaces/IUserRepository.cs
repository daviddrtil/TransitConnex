using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.User;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User, UserUpdateCommand>
{
    IQueryable<User> QueryById(Guid id);
}
