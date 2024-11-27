using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class VehicleService(IVehicleRepository vehicleRepository, ISeatRepository seatRepository) : IVehicleService
{
    public async Task<List<VehicleDto>> GetAllVehicles()
    {
        return await vehicleRepository.QueryAll().ToDto().ToListAsync();
    }

    public async Task<VehicleDto>
        GetVehicleById(Guid id) // TODO -> mby we can reutrn null? -> or based on input prop ? -> throwErrorNotFound boolean?
    {
        return await vehicleRepository.QueryById(id).ToDto().FirstOrDefaultAsync() ??
               throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
        // return (await _vehicleRepository.QueryById(id).ToDto().FirstOrDefaultAsync())!;
    }

    public async Task<Vehicle> CreateVehicle(VehicleCreateCommand createCommand)
    {
        // TODO -> iconId validation?
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
        
        return newVehicle;
    }

    public async Task<List<Vehicle>> CreateVehicles(List<VehicleCreateCommand> createCommands)
    {
        // TODO -> iconId validation? -> private method?
        var newVehicles = new List<Vehicle>();
        var newSeats = new List<Seat>();
        foreach (var createCommand in createCommands)
        {
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
            newVehicles.Add(newVehicle);

            // if (createCommand.NumberOfSeats != 0)
            // {
            //     for (int i = 1; i <= createCommand.NumberOfSeats; i++)
            //     {
            //         var newSeat = new Seat
            //         {
            //              SeatNumber = i,
            //              VehicleId = newVehicle.Id
            //         }
            //     }
            // }
        }
        
        // var createdVehicles = createCommands
        //     .ConvertAll(createCommand => new Vehicle
        //     {
        //         Id = Guid.NewGuid(),
        //         Label = createCommand.Label,
        //         Spz = createCommand.Spz,
        //         Manufacturer = createCommand.Manufacturer,
        //         Capacity = createCommand.Capacity,
        //         VehicleType = createCommand.VehicleType,
        //         IconId = createCommand.IconId,
        //         LineId = createCommand.LineId
        //     });

        await vehicleRepository.AddBatch(newVehicles);
        
        // TODO -> seat creation

        return newVehicles;
    }

    public async Task<Vehicle> EditVehicle(VehicleUpdateCommand editCommand)
    {
        var vehicle = await vehicleRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();

        if (vehicle == null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {editCommand.Id} was not found.");
        }

        // TODO -> think of better way to do this? -> mby reflection?
        // vehicle.Manufacturer = editedVehicle.Manufacturer;
        // vehicle.Capacity = editedVehicle.Capacity;
        // vehicle.Spz = editedVehicle.Spz;
        // vehicle.VehicleType = editedVehicle.VehicleType;
        // vehicle.IconId = editedVehicle.IconId;
        // vehicle.LineId = editedVehicle.LineId;

        await vehicleRepository.Update(vehicle, editCommand);

        return vehicle;
    }

    public async Task DeleteVehicle(Guid id) // TODO -> prolly should be checking if vehicle is assigned somewhere and should have some handling
    {
        var vehicle = await vehicleRepository.QueryById(id).FirstOrDefaultAsync();

        if (vehicle == null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
        }

        await vehicleRepository.Delete(vehicle);
    }

    public async Task DeleteVehicles(List<Guid> ids) // TODO -> prolly should be checking if vehicle is assigned somewhere and should have some handling
    {
        var existingVehicles = await vehicleRepository.QueryExistingIds(ids).ToListAsync();
        var errorIds = ids.Except(existingVehicles.Select(x => x.Id)).ToList();

        if (errorIds.Count != 0)
        {
            throw new KeyNotFoundException($"Given vehicles to be deleted do not exist: [{string.Join(',', errorIds)}]");
        }

        await vehicleRepository.DeleteBatch(existingVehicles);
    }
}
