using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class LocationMongoService(
    ILocationMongoRepository locationRepo,
    IMapper mapper) : ILocationMongoService
{
    public async Task<IEnumerable<Location>> GetAll()
    {
        var locations = await locationRepo.GetAll();
        return mapper.Map<IEnumerable<Location>>(locations);
    }

    public async Task<Location?> GetById(Guid id)
    {
        var location = await locationRepo.GetById(id);
        if (location == null)
        {
            return null;
        }

        return mapper.Map<Location>(location);
    }

    public async Task<Guid> Create(Location location)
    {
        if (location.Id == Guid.Empty)
        {
            location.Id = Guid.NewGuid(); // Always only add
        }

        var locationDoc = mapper.Map<LocationDoc>(location);
        await locationRepo.Upsert(locationDoc);
        return location.Id;
    }

    public async Task Update(Location location)
    {
        var locationDoc = await locationRepo.GetById(location.Id);
        if (locationDoc == null)
        {
            return; // Document not exists, update is not performed
        }

        var newLocationDoc = mapper.Map<LocationDoc>(location);
        await locationRepo.Upsert(newLocationDoc);
    }

    public async Task Delete(Guid id)
    {
        await locationRepo.Delete(id);
    }
}
