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

    public async Task AddUserLocationFavourite(UserLocationFavourite userLocation)
    {
        if (!Db.UserLocationFavourites.Any(x =>
                x.UserId == userLocation.UserId
                && x.LocationId == userLocation.LocationId))
        {
            Db.UserLocationFavourites.Add(userLocation);
            await Db.SaveChangesAsync();
        }
    }

    public async Task AddUserLineFavourite(UserConnectionFavourite userConnection)
    {
        if (!Db.UserConnectionFavourites.Any(x =>
                x.UserId == userConnection.UserId
                && x.FromLocationId == userConnection.FromLocationId
                && x.ToLocationId == userConnection.ToLocationId))
        {
            Db.UserConnectionFavourites.Add(userConnection);
            await Db.SaveChangesAsync();
        }
    }

    public async Task DeleteUserLocationFavourite(UserLocationFavourite userLocation)
    {
        if (Db.UserLocationFavourites.Any(x =>
            x.UserId == userLocation.UserId
            && x.LocationId == userLocation.LocationId))
        {
            Db.Remove(userLocation);
            await Db.SaveChangesAsync();
        }
    }

    public async Task DeleteUserLocationFavourites(List<UserLocationFavourite> userLocationFavourites)
    {
        Db.RemoveRange(userLocationFavourites);
        await Db.SaveChangesAsync();
    }

    public async Task DeleteUserConnectionFavourite(UserConnectionFavourite userConnection)
    {
        if (Db.UserConnectionFavourites.Any(x =>
            x.UserId == userConnection.UserId
            && x.FromLocationId == userConnection.FromLocationId
            && x.ToLocationId == userConnection.ToLocationId))
        {
            Db.Remove(userConnection);
            await Db.SaveChangesAsync();
        }
    }

    public async Task DeleteUserConnectionFavourites(List<UserConnectionFavourite> userConnectionFavourites)
    {
        Db.RemoveRange(userConnectionFavourites);
        await Db.SaveChangesAsync();
    }
}
