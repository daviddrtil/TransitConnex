using TransitConnex.Command.Commands.Service;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class ServiceRepository : BaseRepository<Service, ServiceUpdateCommand>, IServiceRepository
{
    private readonly AppDbContext _db;

    public ServiceRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Service> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }
}
