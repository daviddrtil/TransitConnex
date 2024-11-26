using MongoDB.Driver.Linq;
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

    public IQueryable<UserLocationFavourite> QueryLocationFavouritesByIds(Guid userId, Guid locationId)
    {
        return db.UserLocationFavourites.Where(x => x.UserId == userId && x.LocationId == locationId);
    }

    public IQueryable<UserConnectionFavourite> QueryConnectionFavouritesByIds(Guid userId, Guid fromLocationId, Guid toLocationId)
    {
        return db.UserConnectionFavourites.Where(x => x.UserId == userId && x.FromLocationId == fromLocationId && x.ToLocationId == toLocationId);
    }

    public IQueryable<UserConnectionFavourite> QueryAllUserConnectionFavouritesWithLocationId(Guid locationId)
    {
        return db.UserConnectionFavourites.Where(x => x.FromLocationId == locationId || x.ToLocationId == locationId);
    }

    public Task<bool> EmailExists(string email)
    {
        return QueryAll().AnyAsync(x => x.Email == email);
    }

    public async Task AddUserLocationFavourite(UserLocationFavourite userLocationFavourite)
    {
        db.UserLocationFavourites.Add(userLocationFavourite);
        await db.SaveChangesAsync();
    }

    public async Task AddUserLineFavourite(UserConnectionFavourite userConnection)
    {
        db.UserConnectionFavourites.Add(userConnection);
        await db.SaveChangesAsync();
    }

    public async Task DeleteUserLocationFavourite(UserLocationFavourite userLocationFavourite)
    {
        db.Remove(userLocationFavourite);
        await db.SaveChangesAsync();
    }

    public async Task DeleteUserLocationFavourites(List<UserLocationFavourite> userLocationFavourites)
    {
        db.RemoveRange(userLocationFavourites);
        await db.SaveChangesAsync();
    }

    public async Task DeleteUserConnectionFavourite(UserConnectionFavourite userConnectionFavourite)
    {
        db.Remove(userConnectionFavourite);
        await db.SaveChangesAsync();
    }

    public async Task DeleteUserConnectionFavourites(List<UserConnectionFavourite> userConnectionFavourites)
    {
        db.RemoveRange(userConnectionFavourites);
        await db.SaveChangesAsync();
    }
}
