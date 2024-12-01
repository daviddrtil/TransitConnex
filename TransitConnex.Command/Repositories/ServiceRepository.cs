using TransitConnex.Command.Commands.Service;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class ServiceRepository(AppDbContext db) : BaseRepository<Service, ServiceUpdateCommand>(db), IServiceRepository
{
    public IQueryable<Service> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
