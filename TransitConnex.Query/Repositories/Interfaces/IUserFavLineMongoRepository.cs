using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IUserFavLineMongoRepository : IBaseMongoRepository<UserFavLineDoc, Guid>
{
    Task<IEnumerable<UserFavLineDoc>> GetByUserId(Guid userId);
}
