using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.ScheduledRoute;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class ScheduledRouteService(
    IMapper mapper,
    IScheduledRouteRepository scheduledRouteRepository,
    IRouteRepository routeRepository,
    IVehicleRepository vehicleRepository,
    IRouteTicketRepository routeTicketRepository)
        : IScheduledRouteService
{
    public async Task<List<ScheduledRouteDto>> GetScheduledRoutesFiltered(ScheduledRouteFilteredQuery filter)
    {
        var query = scheduledRouteRepository.QueryAll();

        if (filter.VehicleId != null)
        {
            query = query.Where(c => c.VehicleId == filter.VehicleId);
        }

        if (filter.RouteId != null)
        {
            query = query.Where(c => c.RouteId == filter.RouteId);
        }

        if (filter.StartTime != null)
        {
            query = query.Where(c => c.StartTime >= filter.StartTime);
        }

        if (filter.EndTime != null)
        {
            query = query.Where(c => c.EndTime <= filter.EndTime);
        }
        
        var scheduledRoutes = await query.ToListAsync();
        return mapper.Map<List<ScheduledRouteDto>>(scheduledRoutes);
    }

    public async Task<IEnumerable<ScheduledRoute>> GetAllByRouteId(Guid routeId)
    {
        return await scheduledRouteRepository
            .QueryAllScheduledRoutes()
            .Where(x => x.RouteId == routeId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ScheduledRoute>> GetAllByStopId(Guid stopId)
    {
        return await scheduledRouteRepository
            .QueryAllScheduledRoutes()
            .Where(x => x.Route != null
                && x.Route.Stops.Any(y => y.StopId == stopId))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ScheduledRoute>> GetAllByIds(IEnumerable<Guid> ids)
    {
        return await scheduledRouteRepository
            .QueryAllScheduledRoutes()
            .Where(x => ids.Contains(x.Id))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ScheduledRoute> CreateScheduledRoute(ScheduledRouteCreateCommand createCommand)
    {
        var route = await routeRepository.QueryById(createCommand.RouteId).FirstOrDefaultAsync();
        if (route == null)
        {
            throw new KeyNotFoundException($"Route for specific new scheduled route with ID: " +
                $"{createCommand.RouteId} is not found.");
        }

        if (!await vehicleRepository.Exists(createCommand.VehicleId))
        {
            throw new KeyNotFoundException($"Vehicle for specific new scheduled route with ID: " +
                $"{createCommand.VehicleId} is not found.");
        }

        var newScheduledObj = new ScheduledRoute
        {
            Price = createCommand.Price,
            StartTime = createCommand.StartTime,
            EndTime = createCommand.StartTime.Add(route.DurationTime),
            RouteId = createCommand.RouteId,
            VehicleId = createCommand.VehicleId,
        };
        await scheduledRouteRepository.Add(newScheduledObj);

        // Load all properties
        var newScheduledRoute = await scheduledRouteRepository
            .QueryById(newScheduledObj.Id)
            .AsNoTracking()
            .FirstAsync();

        return newScheduledRoute;
    }

    public async Task<ScheduledRoute> EditScheduledRoute(ScheduledRouteUpdateCommand editCommand)
    {
        var scheduled = await scheduledRouteRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();
        if (scheduled == null)
        {
            throw new KeyNotFoundException($"Scheduled route with ID: {editCommand.Id} is not found.");
        }

        scheduled = mapper.Map(editCommand, scheduled);
        scheduled.EndTime = scheduled.StartTime + scheduled.Route!.DurationTime;
        await scheduledRouteRepository.Update(scheduled);

        return scheduled;
    }

    public async Task DeleteScheduledRoute(Guid id)
    {
        var scheduledRoute = await scheduledRouteRepository.QueryById(id).FirstOrDefaultAsync();
        if (scheduledRoute == null)
        {
            throw new KeyNotFoundException($"Scheduled route with ID: {id} is not found.");
        }

        var reservations = await scheduledRouteRepository.GetReservationsForScheduledRoute(scheduledRoute.Id);
        await scheduledRouteRepository.DeleteReservations(reservations);
        
        var routeTickets = await routeTicketRepository.QueryAll().Where(x => x.ScheduledRouteId == scheduledRoute.Id).ToListAsync();
        await routeTicketRepository.DeleteBatch(routeTickets);
        
        await scheduledRouteRepository.Delete(scheduledRoute);
    }
}
