using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface IUserFavConnectionMongoService
{
    Task<IEnumerable<UserConnectionFavourite>> GetByUserId(Guid id);
    Task<Guid> Add(UserConnectionFavourite connection);
    Task<bool> Remove(UserConnectionFavourite connection);
}
