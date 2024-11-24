using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Stop;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface IStopRepository : IBaseRepository<Stop, StopUpdateCommand>
{
    IQueryable<Stop> QueryById(Guid id);
}
