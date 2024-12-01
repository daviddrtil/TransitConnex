using AutoMapper;
using TransitConnex.API.Handlers.QueryHandlers.Common;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Handlers.QueryHandlers;

public class ServiceQueryHandler(IServiceService serviceServiceSoT) : IBaseQueryHandler<ServiceDto>
{
    public async Task<List<ServiceDto>> HandleGetFiltered(ServiceFilteredQuery filter)
    {
        return await serviceServiceSoT.GetFilteredServices(filter);
    }
}
