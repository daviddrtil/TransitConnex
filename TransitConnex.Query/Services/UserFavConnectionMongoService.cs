using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class UserFavConnectionMongoService(
    IUserFavConnectionMongoRepository userFavConnectionRepo,
    IMapper mapper) : IUserFavConnectionMongoService
{
    public async Task<IEnumerable<UserConnectionFavourite>> GetByUserId(Guid id)
    {
        var connections = await userFavConnectionRepo.GetByUserId(id);
        return mapper.Map<IEnumerable<UserConnectionFavourite>>(connections);
    }

    public async Task<Guid> Add(UserConnectionFavourite connection)
    {
        var connectionDoc = mapper.Map<UserFavConnectionDoc>(connection);
        connectionDoc.Id = Guid.NewGuid();
        await userFavConnectionRepo.Upsert(connectionDoc);
        return connectionDoc.Id;
    }

    public async Task<bool> Remove(UserConnectionFavourite connection)
    {
        var connectionDoc = mapper.Map<UserFavConnectionDoc>(connection);
        return await userFavConnectionRepo.Delete(connectionDoc);
    }
}
