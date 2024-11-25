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

    public async Task<IEnumerable<Guid>> Create(IEnumerable<ScheduledRoute> scheduledRoutes)
    {
        foreach (var scheduledRoute in scheduledRoutes)
        {
            if (scheduledRoute.Id == Guid.Empty)
                scheduledRoute.Id = Guid.NewGuid(); // Always only add
        }

        var srDocs = mapper.Map<IEnumerable<ScheduledRouteDoc>>(scheduledRoutes);
        await scheduledRouteRepo.Upsert(srDocs);
        return srDocs.Select(v => v.Id);
    }

    public async Task Delete(IEnumerable<Guid> ids)
    {
        await scheduledRouteRepo.Delete(ids);
    }
}
