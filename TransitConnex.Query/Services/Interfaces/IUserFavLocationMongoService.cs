using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Services.Interfaces;

public interface IUserFavLocationMongoService
{
    Task<IEnumerable<UserFavLocationDto>> GetAll();
    Task<UserFavLocationDto?> GetByUserId(Guid id);
    Task<Guid> Create(UserFavLocationDto location);
}
