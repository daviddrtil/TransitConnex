using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Service;

namespace TransitConnex.Infrastructure.Services.Interfaces;

public interface IServiceService
{
    Task<List<ServiceDto>> GetAllServices();

    Task<ServiceDto> GetServiceById(Guid id);

    Task<bool> ServiceExists(Guid id);

    Task<Service> CreateService(ServiceCreateCommand createCommand);

    Task<Service> EditService(ServiceUpdateCommand updateCommand);

    Task DeleteService(ServiceDeleteCommand deleteCommand);
}
