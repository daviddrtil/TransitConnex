using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IUserFavLocationMongoRepository : IBaseMongoRepository<UserFavLocationDoc, Guid>
{
    Task<IEnumerable<UserFavLocationDoc>> GetByUserId(Guid userId);
    Task<bool> Delete(UserFavLocationDoc location);
}
