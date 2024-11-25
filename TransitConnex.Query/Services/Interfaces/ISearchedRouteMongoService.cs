using TransitConnex.Domain.DTOs;

namespace TransitConnex.Query.Services.Interfaces;

public interface ISearchedRouteMongoService
{
    Task<IEnumerable<SearchedRouteDto>> GetAll();
    Task<SearchedRouteDto?> GetById(Guid id);
    Task<Guid> Create(SearchedRouteDto searchedRoute);
    Task Update(SearchedRouteDto searchedRoute);
    Task Delete(Guid id);

    Task<IEnumerable<Guid>> Create(IEnumerable<SearchedRouteDto> searchedRoutes);
    Task Delete(IEnumerable<Guid> ids);
}
