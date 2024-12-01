using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class LocationMongoService(
    ILocationMongoRepository locationRepo,
    IMapper mapper) : ILocationMongoService
{
    public async Task<IEnumerable<LocationDto>> GetAll()
    {
        var locations = await locationRepo.GetAll();
        return mapper.Map<IEnumerable<LocationDto>>(locations);
    }

    public async Task<LocationDto?> GetById(Guid id)
    {
        var location = await locationRepo.GetById(id);
        if (location == null)
        {
            return null;
        }

        return mapper.Map<LocationDto>(location);
    }

    public async Task<IEnumerable<LocationDto>> GetByName(string name)
    {
        var locationDocs = await locationRepo.GetByName(name);
        return mapper.Map<IEnumerable<LocationDto>>(locationDocs);
    }

    public async Task<LocationDto?> GetClosest(double latitude, double longitude)
    {
        var locationDoc = await locationRepo.GetClosest(latitude, longitude);
        if (locationDoc == null)
            return null;
        return mapper.Map<LocationDto>(locationDoc);
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

    public async Task<IEnumerable<Guid>> Create(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
        {
            if (location.Id == Guid.Empty)
                location.Id = Guid.NewGuid(); // Always only add
        }

        var locationDocs = mapper.Map<IEnumerable<LocationDoc>>(locations);
        await locationRepo.Upsert(locationDocs);
        return locationDocs.Select(v => v.Id);
    }

    public async Task Update(IEnumerable<Location> locations)
    {
        var locationDocs = mapper.Map<IEnumerable<LocationDoc>>(locations);
        if (!locationDocs.Any())
            return;
        await locationRepo.Upsert(locationDocs);
    }

    public async Task Delete(IEnumerable<Guid> ids)
    {
        if (!ids.Any())
            return;
        await locationRepo.Delete(ids);
    }
}
