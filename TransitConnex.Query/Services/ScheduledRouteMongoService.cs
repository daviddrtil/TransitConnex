using AutoMapper;
using MongoDB.Driver;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class ScheduledRouteMongoService(
    IMapper mapper,
    ILocationMongoRepository locationRepo,
    IScheduledRouteMongoRepository scheduledRouteRepo) : IScheduledRouteMongoService
{
    // todo should return ScheduledRoute, not dto
    public async Task<IEnumerable<ScheduledRouteDto>> GetScheduledRoutes(
        Guid startLocationId, Guid endLocationId, DateTime startTime)
    {
        var startLocation = await locationRepo.GetById(startLocationId);
        var endLocation = await locationRepo.GetById(endLocationId);
        if (startLocation is null || endLocation is null)
            return [];
        var srDocs = await scheduledRouteRepo.GetAll(
            startLocation.Stops, endLocation.Stops, startTime);
        foreach (var sr in srDocs)
        {
            sr.StartTime = sr.StartTime.ToLocalTime();
            sr.EndTime = sr.EndTime.ToLocalTime();
            foreach (var stop in sr.Stops)
            {
                stop.DepartureTime = stop.DepartureTime.ToLocalTime();
            }
        }
        return mapper.Map<IEnumerable<ScheduledRouteDto>>(srDocs);
    }

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

    public async Task Update(IEnumerable<ScheduledRoute> scheduledRoutes)
    {
        var srDocs = mapper.Map<IEnumerable<ScheduledRouteDoc>>(scheduledRoutes);
        if (!srDocs.Any())
            await scheduledRouteRepo.Upsert(srDocs);
    }

    public async Task Delete(IEnumerable<Guid> ids)
    {
        await scheduledRouteRepo.Delete(ids);
    }
}
