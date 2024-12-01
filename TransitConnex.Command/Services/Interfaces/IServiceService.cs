using TransitConnex.Command.Commands.Service;
using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface IServiceService
{
    Task<List<ServiceDto>> GetFilteredServices(ServiceFilteredQuery filter);

    Task<Service> CreateService(ServiceCreateCommand createCommand);
    Task<Service> EditService(ServiceUpdateCommand updateCommand);
    Task DeleteService(Guid id); 
}
