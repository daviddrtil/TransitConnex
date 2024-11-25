using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Domain.DTOs;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class SearchedRouteQueryHandler(
    IMapper mapper,
    ISearchedRouteMongoService searchedRouteService) : IBaseQueryHandler<SearchedRouteDto>
{
}
