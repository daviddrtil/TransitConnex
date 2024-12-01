using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Route;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class RouteService(IMapper mapper, IRouteRepository routeRepository, IStopRepository stopRepository, ILineRepository lineRepository) : IRouteService
{
    public async Task<List<RouteDto>> GetRoutesFiltered()
    {
        throw new NotImplementedException();
    }

    public async Task<Route> CreateRoute(RouteCreateCommand createCommand)
    {
        if (!await stopRepository.Exists(createCommand.EndStopId))
        {
            throw new KeyNotFoundException($"End stop with ID: {createCommand.EndStopId} does not exist");
        }

        if (!await stopRepository.Exists(createCommand.StartStopId))
        {
            throw new KeyNotFoundException($"Start stop with ID: {createCommand.StartStopId} does not exist");
        }

        if (!await lineRepository.Exists(createCommand.LineId))
        {
            throw new KeyNotFoundException($"Line with ID: {createCommand.LineId} does not exist");
        }
        
        var newRoute = mapper.Map<Route>(createCommand);
        await routeRepository.Add(newRoute);

        var newRouteStops = createCommand.Stops
            .Select(stop => new RouteStop
            {
                RouteId = newRoute.Id, 
                StopId = stop.StopId, 
                StopOrder = stop.StopOrder, 
                TimeDurationFromFirstStop = stop.TimeDurationFromFirstStop
            }).ToList();
        await routeRepository.AddRouteStops(newRouteStops);
        
        return newRoute;
    }

    public async Task<Route> EditRoute(RouteUpdateCommand editCommand)
    {
        var route = await routeRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();
        if (route == null)
        {
            throw new KeyNotFoundException($"Route with ID: {editCommand.Id} does not exist");
        }
        
        route = mapper.Map(editCommand, route);
        await routeRepository.Update(route);
        
        return route;
    }

    public async Task DeleteRoute(Guid id)
    {
        var route = await routeRepository.QueryById(id).FirstOrDefaultAsync();
        if (route == null)
        {
            throw new KeyNotFoundException($"Route with ID: {id} was not found");
        }
        
        await routeRepository.Delete(route);
    }
    
    public async Task AddStopToRoute(RouteStopAddCommand addCommand)
    {
        var route = await routeRepository.QueryById(addCommand.RouteId).FirstOrDefaultAsync();
        if (route == null)
        {
            throw new KeyNotFoundException($"Route with ID: {addCommand.RouteId} not found.");
        }
        
        var stop = await stopRepository.QueryById(addCommand.StopId).FirstOrDefaultAsync();
        if (stop == null)
        {
            throw new KeyNotFoundException($"Stop with ID: {addCommand.StopId} not found.");
        }

        if (addCommand.StopOrder < 0)
        {
            throw new ArgumentException($"Stop order must be greater than or equal to 0. Stop order: {addCommand.StopOrder}.");
        }

        if (addCommand.StopOrder == 0)
        {
            if (addCommand.Delta == TimeSpan.Zero)
            {
                throw new ArgumentException(
                    $"When adding new start stop to route you need to add delta to first next stop. Your delta is set to zero.");
            }

            if (addCommand.TimeDurationFromFirstStop != TimeSpan.Zero)
            {
                throw new ArgumentException($"When adding new start stop, time duration from first stop must be set to zero.");
            }
        }

        RouteStop newRouteStop = new RouteStop
        {
            RouteId = route.Id, StopId = stop.Id, TimeDurationFromFirstStop = addCommand.TimeDurationFromFirstStop,
        };
        var path = await routeRepository.QueryRoutePath(route.Id).ToListAsync();
        var last = false;
        if (path.Count != 0) // TODO -> could validate TimeDurationFromFirstStop
        {
            if (addCommand.StopOrder >= path.Count)
            {
                newRouteStop.StopOrder = path.Count;
                last = true;
            }
            else
            {
                newRouteStop.StopOrder = addCommand.StopOrder;
                foreach (var routeStop in path.Where(routeStop => routeStop.StopOrder >= addCommand.StopOrder))
                {
                    routeStop.StopOrder++;
                    routeStop.TimeDurationFromFirstStop += addCommand.Delta;
                }
            }
        }
        else
        {
            newRouteStop.StopOrder = 0;
        }
        
        path.Add(newRouteStop);
        await routeRepository.UpsertBatchRouteStops(path);

        if (addCommand.StopOrder == 0)
        {
            route.StartStopId = stop.Id;
        }
        
        if (last)
        {
            route.EndStopId = stop.Id;
        }
        
        await routeRepository.Update(route);
    }

    public async Task RemoveStopFromRoute(RouteStopRemoveCommand removeCommand)
    {
        var route = await routeRepository.QueryById(removeCommand.RouteId).FirstOrDefaultAsync();
        if (route == null)
        {
            throw new KeyNotFoundException($"Route with ID: {removeCommand.RouteId} not found.");
        }
        
        var stop = await stopRepository.QueryById(removeCommand.StopId).FirstOrDefaultAsync();
        if (stop == null)
        {
            throw new KeyNotFoundException($"Stop with ID: {removeCommand.StopId} not found.");
        }
        
        var routeStopToBeRemoved = await routeRepository.QueryRouteStops(route.Id, stop.Id).FirstOrDefaultAsync();
        if (routeStopToBeRemoved == null)
        {
            throw new KeyNotFoundException($"Stop trying to remove from route is already not in route. Stop ID: {removeCommand.StopId}, Route ID: {route.Id}");
        }
        
        var path = await routeRepository.QueryRoutePath(route.Id).ToListAsync();
        if (path.Count <= 2)
        {
            throw new ArgumentException(
                $"Route cannot have less than 2 stops, if you want to delete this stop from route, you need to add replacement first.");
        }
        
        if (routeStopToBeRemoved.StopOrder == 0)
        {
            var replacementId = path.FirstOrDefault(rs => rs.StopOrder == 1)!.StopId;
            route.StartStopId = replacementId;
        }
        
        if (routeStopToBeRemoved.StopOrder == path.Count - 1)
        {
            var replacementId = path.FirstOrDefault(rs => rs.StopOrder == path.Count - 2)!.StopId;
            route.EndStopId = replacementId;
        }

        await routeRepository.Update(route);
        
        var decrement = 0;
        var delta = TimeSpan.Zero;
        var getDelta = false;
        foreach (var routeStop in path)
        {
            if (routeStop.StopId == removeCommand.StopId)
            {
                if (routeStop.StopOrder == 0) // first deleted
                {
                    getDelta = true;
                }
                decrement++;
            }
            else
            {
                if (getDelta) // setting start after previous removed + setting delta
                {
                    delta = routeStop.TimeDurationFromFirstStop;
                    getDelta = false;
                }
                
                routeStop.StopOrder -= decrement;
                routeStop.TimeDurationFromFirstStop -= delta;
            }
        }
        
        await routeRepository.UpsertBatchRouteStops(path);
        await routeRepository.DeleteRouteStop(routeStopToBeRemoved);
    }
}
