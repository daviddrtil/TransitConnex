using MongoDB.Driver.Linq;
using TransitConnex.Command.Commands.User;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class UserRepository(AppDbContext db) : BaseRepository<User, UserUpdateCommand>(db), IUserRepository
{
    private readonly AppDbContext Db = db;

    public IQueryable<User> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public IQueryable<UserLocationFavourite> QueryLocationFavouritesByIds(Guid userId, Guid locationId)
    {
        return Db.UserLocationFavourites.Where(x => x.UserId == userId && x.LocationId == locationId);
    }

    public IQueryable<UserConnectionFavourite> QueryConnectionFavouritesByIds(Guid userId, Guid fromLocationId, Guid toLocationId)
    {
        return Db.UserConnectionFavourites.Where(x => x.UserId == userId && x.FromLocationId == fromLocationId && x.ToLocationId == toLocationId);
    }

    public IQueryable<UserConnectionFavourite> QueryAllUserConnectionFavouritesWithLocationId(Guid locationId)
    {
        return Db.UserConnectionFavourites.Where(x => x.FromLocationId == locationId || x.ToLocationId == locationId);
    }

    public Task<bool> EmailExists(string email)
    {
        return QueryAll().AnyAsync(x => x.Email == email);
    }

    public async Task AddUserLocationFavourite(UserLocationFavourite userLocationFavourite)
    {
        Db.UserLocationFavourites.Add(userLocationFavourite);
        await Db.SaveChangesAsync();
    }

    public async Task AddUserLineFavourite(UserConnectionFavourite userConnection)
    {
        Db.UserConnectionFavourites.Add(userConnection);
        await Db.SaveChangesAsync();
    }

    public async Task DeleteUserLocationFavourite(UserLocationFavourite userLocationFavourite)
    {
        Db.Remove(userLocationFavourite);
        await Db.SaveChangesAsync();
    }

    public async Task DeleteUserLocationFavourites(List<UserLocationFavourite> userLocationFavourites)
    {
        Db.RemoveRange(userLocationFavourites);
        await Db.SaveChangesAsync();
    }

    public async Task DeleteUserConnectionFavourite(UserConnectionFavourite userConnectionFavourite)
    {
        Db.Remove(userConnectionFavourite);
        await Db.SaveChangesAsync();
    }

    public async Task DeleteUserConnectionFavourites(List<UserConnectionFavourite> userConnectionFavourites)
    {
        Db.RemoveRange(userConnectionFavourites);
        await Db.SaveChangesAsync();
    }
}
