using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Vehicle;
using TransitConnex.Infrastructure.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class VehicleCommandHandler(IVehicleService vehicleService, IVehicleMongoService vehicleMongoService)
    : IBaseCommandHandler<IVehicleCommand>
{
    public async Task<Guid> HandleCreate(IVehicleCommand command)
    {
        if (command is not VehicleCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected VehicleCreatedCommand.");
        }

        var newVehicle = await vehicleService.CreateVehicle(createCommand);
        var id = await vehicleMongoService.Create(newVehicle);
        
        // TODO -> sync with mongo

        return new Guid();
    }

    public async Task HandleUpdate(IVehicleCommand command)
    {
        if (command is not VehicleUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected VehicleUpdateCommand.");
        }

        var updatedVehicle = await vehicleService.EditVehicle(updateCommand);
        
        // TODO -> sync with mongo
    }

    public async Task HandleDelete(IVehicleCommand command)
    {
        if (command is not VehicleDeleteCommand deleteCommand)
        {
            throw new InvalidCastException("Invalid command given, expected VehicleDeleteCommand.");
        }

        await vehicleService.DeleteVehicle(deleteCommand.Id);
    }
}
