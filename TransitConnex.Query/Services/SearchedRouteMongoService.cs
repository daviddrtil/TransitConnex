using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class SearchedRouteMongoService(
    ISearchedRouteMongoRepository searchedRouteRepo,
    IMapper mapper) : ISearchedRouteMongoService
{
    public async Task<IEnumerable<SearchedRouteDto>> GetAll()
    {
        var srDocs = await searchedRouteRepo.GetAll();
        return mapper.Map<IEnumerable<SearchedRouteDto>>(srDocs);
    }

    public async Task<IEnumerable<SearchedRouteDto>> GetByUserId(Guid userId)
    {
        var searchedRouteDocs = await searchedRouteRepo.GetByUserId(userId);
        return mapper.Map<IEnumerable<SearchedRouteDto>>(searchedRouteDocs);
    }

    public async Task<Guid> Create(SearchedRouteDto sr)
    {
        if (sr.Id == Guid.Empty)
        {
            sr.Id = Guid.NewGuid(); // Always only add
        }

        var srDoc = mapper.Map<SearchedRouteDoc>(sr);
        await searchedRouteRepo.Upsert(srDoc);
        return sr.Id;
    }

    public async Task Update(SearchedRouteDto sr)
    {
        var srDoc = await searchedRouteRepo.GetById(sr.Id);
        if (srDoc == null)
        {
            return; // Document not exists, update is not performed
        }

        var newSRDoc = mapper.Map<SearchedRouteDoc>(sr);
        await searchedRouteRepo.Upsert(newSRDoc);
    }

    public async Task Delete(Guid id)
    {
        await searchedRouteRepo.Delete(id);
    }

    public async Task<IEnumerable<Guid>> Create(IEnumerable<SearchedRouteDto> searchedRoutes)
    {
        foreach (var searchedRoute in searchedRoutes)
        {
            if (searchedRoute.Id == Guid.Empty)
                searchedRoute.Id = Guid.NewGuid(); // Always only add
        }

        var srDocs = mapper.Map<IEnumerable<SearchedRouteDoc>>(searchedRoutes);
        await searchedRouteRepo.Upsert(srDocs);
        return srDocs.Select(v => v.Id);
    }

    public async Task Delete(IEnumerable<Guid> ids)
    {
        await searchedRouteRepo.Delete(ids);
    }
}
