using TransitConnex.Domain.Models;

namespace TransitConnex.Query.Services.Interfaces;

public interface IUserFavLocationMongoService
{
    Task<IEnumerable<UserLocationFavourite>> GetByUserId(Guid userId);
    Task<Guid> Add(UserLocationFavourite location);
    Task<bool> Remove(UserLocationFavourite location);
}
