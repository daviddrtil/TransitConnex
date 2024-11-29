using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class RouteStopMongoService(
    IRouteStopMongoRepository routeStopRepo,
    IMapper mapper) : IRouteStopMongoService
{
    //public async Task<IEnumerable<Guid>> Create(IEnumerable<RouteStop> routeStops)
    //{
    //    var routeStopDocs = mapper.Map<IEnumerable<RouteStopDoc>>(routeStops);
    //    foreach (var routeStopDoc in routeStopDocs)
    //    {
    //        if (routeStopDoc.Id == Guid.Empty)
    //            routeStopDoc.Id = Guid.NewGuid();
    //    }
    //    await routeStopRepo.Upsert(routeStopDocs);
    //    return routeStopDocs.Select(rs => rs.Id);
    //}
}
