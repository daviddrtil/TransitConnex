using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Services.Interfaces;

public interface IUserFavLineMongoService
{
    Task<IEnumerable<UserFavLineDto>> GetAll();
    Task<UserFavLineDto?> GetByUserId(Guid id);
    Task<Guid> Create(UserFavLineDto line);
}
