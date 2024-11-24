using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs.SearchedRoute;
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

    public async Task<SearchedRouteDto?> GetById(Guid id)
    {
        var sr = await searchedRouteRepo.GetById(id);
        if (sr == null)
        {
            return null;
        }

        return mapper.Map<SearchedRouteDto>(sr);
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
}
