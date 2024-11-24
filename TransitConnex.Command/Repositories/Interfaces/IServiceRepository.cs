using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Service;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface IServiceRepository : IBaseRepository<Service, ServiceUpdateCommand>
{
    IQueryable<Service> QueryById(Guid id);
}
