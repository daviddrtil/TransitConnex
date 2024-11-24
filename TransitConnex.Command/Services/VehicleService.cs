using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
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

    public async Task<bool> VehicleExists(Guid id)
    {
        return await vehicleRepository.QueryById(id).AnyAsync();
    }

    public async Task<Vehicle> CreateVehicle(VehicleCreateCommand createCommand)
    {
        // TODO -> iconId validation?
        var newVehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Label = createCommand.Label,
            Spz = createCommand.Spz,
            Manufacturer = createCommand.Manufacturer,
            Capacity = createCommand.Capacity,
            VehicleType = createCommand.VehicleType,
            IconId = createCommand.IconId,
            LineId = createCommand.LineId
        };

        await vehicleRepository.Add(newVehicle);

        return newVehicle;
    }

    public async Task<List<Vehicle>> CreateVehicles(List<VehicleCreateCommand> createCommands)
    {
        // TODO -> iconId validation? -> private method?
        var createdVehicles = createCommands
            .ConvertAll(createCommand => new Vehicle
            {
                Id = Guid.NewGuid(),
                Label = createCommand.Label,
                Spz = createCommand.Spz,
                Manufacturer = createCommand.Manufacturer,
                Capacity = createCommand.Capacity,
                VehicleType = createCommand.VehicleType,
                IconId = createCommand.IconId,
                LineId = createCommand.LineId
            });

        await vehicleRepository.AddBatch(createdVehicles);

        return createdVehicles;
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
