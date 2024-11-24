using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class ScheduledRouteMongoService(
    IScheduledRouteMongoRepository scheduledRouteRepo,
    IMapper mapper) : IScheduledRouteMongoService
{
    public async Task<IEnumerable<ScheduledRoute>> GetAll()
    {
        var srDocs = await scheduledRouteRepo.GetAll();
        return mapper.Map<IEnumerable<ScheduledRoute>>(srDocs);
    }

    public async Task<ScheduledRoute?> GetById(Guid id)
    {
        var sr = await scheduledRouteRepo.GetById(id);
        if (sr == null)
        {
            return null;
        }

        return mapper.Map<ScheduledRoute>(sr);
    }

    public async Task<Guid> Create(ScheduledRoute sr)
    {
        if (sr.Id == Guid.Empty)
        {
            sr.Id = Guid.NewGuid(); // Always only add
        }

        var srDoc = mapper.Map<ScheduledRouteDoc>(sr);
        await scheduledRouteRepo.Upsert(srDoc);
        return sr.Id;
    }

    public async Task Update(ScheduledRoute sr)
    {
        var srDoc = await scheduledRouteRepo.GetById(sr.Id);
        if (srDoc == null)
        {
            return; // Document not exists, update is not performed
        }

        var newSRDoc = mapper.Map<ScheduledRouteDoc>(sr);
        await scheduledRouteRepo.Upsert(newSRDoc);
    }

    public async Task Delete(Guid id)
    {
        await scheduledRouteRepo.Delete(id);
    }
}
