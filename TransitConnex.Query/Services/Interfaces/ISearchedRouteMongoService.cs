using TransitConnex.Domain.DTOs.SearchedRoute;

namespace TransitConnex.Query.Services.Interfaces;

public interface ISearchedRouteMongoService
{
    // todo SearchedRouteDto rewrite to model?
    Task<IEnumerable<SearchedRouteDto>> GetAll();
    Task<SearchedRouteDto?> GetById(Guid id);
    Task<Guid> Create(SearchedRouteDto searchedRoute);
    Task Update(SearchedRouteDto searchedRoute);
    Task Delete(Guid id);
}
