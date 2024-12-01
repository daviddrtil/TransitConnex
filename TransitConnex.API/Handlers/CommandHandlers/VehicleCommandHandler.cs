using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class VehicleCommandHandler(IVehicleService vehicleService, IVehicleMongoService vehicleMongoService)
    : IBaseCommandHandler<IVehicleCommand>
{
    public async Task<Guid> HandleCreate(IVehicleCommand command)
    {
        if (command is not VehicleCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(VehicleCreateCommand)}.");
        }
        var newVehicle = await vehicleService.CreateVehicle(createCommand);
        await vehicleMongoService.Create(newVehicle);
        return newVehicle.Id;
    }
    
    public async Task<List<Guid>> HandleBatchCreate(IVehicleCommand command)
    {
        if (command is not VehicleBatchCreateCommand createCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(VehicleBatchCreateCommand)}.");
        }
        var newVehicles = await vehicleService.CreateVehicles(createCommand.Vehicles);
        await vehicleMongoService.Create(newVehicles);
        return newVehicles.Select(v => v.Id).ToList();
    }

    public async Task HandleUpdate(IVehicleCommand command)
    {
        if (command is not VehicleUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected VehicleUpdateCommand.");
        }
        var updatedVehicle = await vehicleService.EditVehicle(updateCommand);
        await vehicleMongoService.Update(updatedVehicle);
    }

    public async Task HandleDelete(Guid id)
    {
        await vehicleService.DeleteVehicle(id);
        await vehicleMongoService.Delete(id);
    }

    public async Task HandleReplace(IVehicleCommand command)
    {
        if (command is not VehicleReplaceOnScheduledCommand replaceCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(VehicleReplaceOnScheduledCommand)}.");
        }
        
        await vehicleService.ReplaceVehicleOnScheduledRoutes(replaceCommand);
    }
}
