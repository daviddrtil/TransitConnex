using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Service;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.Infrastructure.Repositories;

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
