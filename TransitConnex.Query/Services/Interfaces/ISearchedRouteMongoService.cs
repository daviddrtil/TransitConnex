using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Services.Interfaces;

public interface ISearchedRouteMongoService
{
    Task<IEnumerable<SearchedRouteDto>> GetAll();
    Task<IEnumerable<SearchedRouteDto>> GetByUserId(Guid userId);
    Task<Guid> Create(SearchedRouteDto searchedRoute);
    Task Update(SearchedRouteDto searchedRoute);
    Task Delete(Guid id);

    Task<IEnumerable<Guid>> Create(IEnumerable<SearchedRouteDto> searchedRoutes);
    Task Delete(IEnumerable<Guid> ids);
}
