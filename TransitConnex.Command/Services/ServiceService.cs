using MongoDB.Driver.Linq;
using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Service;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    public Task<List<ServiceDto>> GetAllServices()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceDto> GetServiceById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ServiceExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Service> CreateService(ServiceCreateCommand createCommand)
    {
        var newService = new Service
        {
            Id = Guid.NewGuid(),
            IconId = createCommand.IconId,
            Description = createCommand.Description,
            Name = createCommand.Name
        };

        await serviceRepository.Add(newService);

        return newService;
    }

    public async Task<Service> EditService(ServiceUpdateCommand updateCommand)
    {
        var service = await serviceRepository.QueryById(updateCommand.Id).FirstOrDefaultAsync();

        if (service == null)
        {
            throw new KeyNotFoundException($"Icon with ID {updateCommand.Id} was not found.");
        }

        await serviceRepository.Update(service, updateCommand);

        return service;
    }

    public async Task DeleteService(ServiceDeleteCommand deleteCommand)
    {
        var service = await serviceRepository.QueryById(deleteCommand.Id).FirstOrDefaultAsync();

        if (service == null)
        {
            throw new KeyNotFoundException($"Icon with ID {deleteCommand.Id} was not found.");
        }

        await serviceRepository.Delete(service);
    }
}
