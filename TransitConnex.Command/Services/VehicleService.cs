using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class VehicleService(IMapper mapper,
    IVehicleRepository vehicleRepository, 
    ISeatRepository seatRepository, 
    IScheduledRouteRepository scheduledRouteRepository, 
    IIconRepository iconRepository,
    ILineRepository lineRepository
    ) : IVehicleService
{
    public async Task<List<VehicleDto>> GetAllVehicles()
    {
        return await vehicleRepository.QueryAll().ToDto().ToListAsync();
    }
    
    public async Task<VehicleDto>
        GetVehicleById(Guid id)
    {
        return await vehicleRepository.QueryById(id).ToDto().FirstOrDefaultAsync() ??
               throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
    }

    public async Task<Vehicle> CreateVehicle(VehicleCreateCommand createCommand)
    {
        if (createCommand.IconId != null && !await iconRepository.Exists(createCommand.IconId.Value))
        {
            throw new KeyNotFoundException($"Icon with ID {createCommand.IconId} was not found.");    
        }

        if (createCommand.LineId != null && !await lineRepository.Exists(createCommand.LineId.Value))
        {
            throw new KeyNotFoundException($"Line with ID {createCommand.LineId} was not found.");
        }
        
        var newVehicle = new Vehicle
        {
            Label = createCommand.Label,
            Spz = createCommand.Spz,
            Manufacturer = createCommand.Manufacturer,
            Capacity = createCommand.Capacity,
            VehicleType = createCommand.VehicleType,
            IconId = createCommand.IconId,
            LineId = createCommand.LineId
        };
        await vehicleRepository.Add(newVehicle);

        if (createCommand.NumberOfSeats != 0)
        {
            var newSeats = new List<Seat>();
            var vagonId = 1;
            for (int i = 1; i <= createCommand.NumberOfSeats; i++)
            {
                var newSeat = new Seat
                {
                    SeatNumber = i,
                    VehicleId = newVehicle.Id,
                    VagonNumber = createCommand.SeatsPerVagon > 0 ? vagonId : 0
                };
                newSeats.Add(newSeat);
                
                if (createCommand.SeatsPerVagon > 0 && i % createCommand.SeatsPerVagon == 0)
                {
                    vagonId++;
                }
            }
            
            await seatRepository.AddBatch(newSeats);
        }

        if (createCommand.Services != null)
        {
            var vehicleServices = createCommand.Services 
                .Select(service => 
                    new VehicleOfferedService
                    {
                        VehicleId = newVehicle.Id, 
                        ServiceId = service
                    }
                )
                .ToList();
            await vehicleRepository.AddServicesToVehicle(vehicleServices);
        }
        
        return newVehicle;
    }

    public async Task<Vehicle> EditVehicle(VehicleUpdateCommand editCommand)
    {
        var vehicle = await vehicleRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();

        if (vehicle == null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {editCommand.Id} was not found.");
        }
        
        vehicle = mapper.Map(editCommand, vehicle);
        await vehicleRepository.Update(vehicle);

        return vehicle;
    }

    public async Task DeleteVehicle(Guid id)
    {
        var vehicle = await vehicleRepository.QueryById(id).FirstOrDefaultAsync();

        if (vehicle == null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
        }
            
        var scheduledRoutes = vehicleRepository.QueryByVehicleScheduledRoutes(vehicle.Id);
        if (scheduledRoutes.Any())
        {
            throw new ArgumentException($"Vehicle with ID {id} has scheduled routes and cannot be deleted. Replace vehicle on routes first.");
        }

        await vehicleRepository.Delete(vehicle);
    }

    public async Task<IEnumerable<ScheduledRoute>> ReplaceVehicleOnScheduledRoutes(
        VehicleReplaceOnScheduledCommand replaceCommand)
    {
        var replaced = await vehicleRepository.QueryById(replaceCommand.ReplacedId).FirstOrDefaultAsync();
        if (replaced == null)
        {
            throw new KeyNotFoundException($"Vehicle to be replaced with ID: {replaceCommand.ReplacedId} was not found.");
        }

        var replacedBy = await vehicleRepository.QueryById(replaceCommand.ReplacedById).FirstOrDefaultAsync();
        if (replacedBy == null)
        {
            throw new KeyNotFoundException($"Vehicle used for replacing with ID: {replaceCommand.ReplacedId} was not found.");
        }

        if (replacedBy.Capacity < replaced.Capacity)
        {
            throw new ArgumentException($"New vehicle must have at least {replaced.Capacity} capacity.");
        }
        
        var scheduledRoutes = await vehicleRepository.QueryByVehicleScheduledRoutes(replaced.Id).ToListAsync();
        foreach (var scheduledRoute in scheduledRoutes)
        {
            scheduledRoute.VehicleId = replacedBy.Id;
        }

        var replacedSeats = await seatRepository.QueryAll().Where(seat => seat.VehicleId == replaced.Id).ToListAsync();
        var replacedBySeats = await seatRepository.QueryAll().Where(seat => seat.VehicleId == replacedBy.Id).ToListAsync();
        var taken = new List<Seat>();
        var updatedReservations = new List<ScheduledRouteSeat>();
        foreach (var replacedSeat in replacedSeats)
        {
            var replacedBySeat = replacedBySeats.FirstOrDefault(seat => seat.SeatNumber == replacedSeat.SeatNumber);
            if (replacedBySeat == null)
            {
                if (replaced.Capacity == replacedBy.Capacity)
                {
                    throw new Exception(
                        $"Error when trying to map reservations for vehicle seats! Seat numbers are out of order! replaced seatNumber: {replacedSeat.SeatNumber}.");
                }

                var firstUnmappedOutReserveSeat = replacedBySeats.FirstOrDefault(seat => seat.SeatNumber > replaced.Capacity && !taken.Contains(seat));
                if (firstUnmappedOutReserveSeat == null)
                {
                    throw new Exception(
                        $"Error when trying to map reservations for vehicle seats! Seat numbers are out of order! replaced seatNumber: {replacedSeat.SeatNumber}.");
                }
                    
                replacedBySeat = firstUnmappedOutReserveSeat;
            }
            
            taken.Add(replacedBySeat);
            var replacedSeatsReservations = await seatRepository.QuerySeatReservations(replacedSeat.Id);
            foreach (var reservation in replacedSeatsReservations)
            {
                reservation.SeatId = replacedBySeat.Id;
            }

            updatedReservations.AddRange(replacedSeatsReservations);
        }
        
        await scheduledRouteRepository.UpsertBatch(scheduledRoutes);
        await seatRepository.UpsertReservations(updatedReservations);

        return scheduledRoutes;
    }
}
