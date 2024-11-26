using TransitConnex.Command.Commands.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User, UserUpdateCommand>
{
    IQueryable<User> QueryById(Guid id);
    IQueryable<UserLocationFavourite> QueryLocationFavouritesByIds(Guid userId, Guid locationId);
    IQueryable<UserConnectionFavourite> QueryConnectionFavouritesByIds(Guid userId, Guid fromLocationId, Guid toLocationId);
    IQueryable<UserConnectionFavourite> QueryAllUserConnectionFavouritesWithLocationId(Guid locationId);
    Task<bool> EmailExists(string email);
    Task AddUserLocationFavourite(UserLocationFavourite userLocationFavourite);
    Task AddUserLineFavourite(UserConnectionFavourite userConnection);
    Task DeleteUserLocationFavourite(UserLocationFavourite userLocationFavourite);
    Task DeleteUserLocationFavourites(List<UserLocationFavourite> userLocationFavourites);
    Task DeleteUserConnectionFavourite(UserConnectionFavourite userConnectionFavourite);
    Task DeleteUserConnectionFavourites(List<UserConnectionFavourite> userConnectionFavourites);
}
