using TransitConnex.Domain.Collections;

namespace TransitConnex.Query.Repositories.Interfaces;

public interface IUserFavConnectionMongoRepository : IBaseMongoRepository<UserFavConnectionDoc, Guid>
{
    Task<IEnumerable<UserFavConnectionDoc>> GetByUserId(Guid userId);
    Task<bool> Delete(UserFavConnectionDoc connection);
}
