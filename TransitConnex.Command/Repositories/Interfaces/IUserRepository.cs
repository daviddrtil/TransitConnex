using TransitConnex.Command.Commands.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User, UserUpdateCommand>
{
    IQueryable<User> QueryById(Guid id);
}
