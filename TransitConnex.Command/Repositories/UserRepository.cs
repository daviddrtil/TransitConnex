using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.User;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : BaseRepository<User, UserUpdateCommand>(db), IUserRepository
{
    public IQueryable<User> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
