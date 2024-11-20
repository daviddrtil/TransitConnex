using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Repositories.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        IQueryable<Service> QueryById(Guid id);
    }
}
