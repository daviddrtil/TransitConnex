using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class UserFavLocationMongoService(
    IUserFavLocationMongoRepository userFavLocationRepo,
    IMapper mapper) : IUserFavLocationMongoService
{
    public async Task<IEnumerable<UserLocationFavourite>> GetByUserId(Guid id)
    {
        var locations = await userFavLocationRepo.GetByUserId(id);
        return mapper.Map<IEnumerable<UserLocationFavourite>>(locations);
    }

    public async Task<Guid> Add(UserLocationFavourite location)
    {
        var locationDoc = mapper.Map<UserFavLocationDoc>(location);
        locationDoc.Id = Guid.NewGuid();
        await userFavLocationRepo.Upsert(locationDoc);
        return locationDoc.Id;
    }

    public async Task<bool> Remove(UserLocationFavourite location)
    {
        var locationDoc = mapper.Map<UserFavLocationDoc>(location);
        return await userFavLocationRepo.Delete(locationDoc);
    }
}
