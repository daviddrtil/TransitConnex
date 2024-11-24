using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class UserRepository(AppDbContext db) : BaseRepository<User, UserUpdateCommand>(db), IUserRepository
{
    public IQueryable<User> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
