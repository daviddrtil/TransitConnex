using TransitConnex.Command.Commands.Service;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IServiceRepository : IBaseRepository<Service, ServiceUpdateCommand>
{
    IQueryable<Service> QueryById(Guid id);
}
