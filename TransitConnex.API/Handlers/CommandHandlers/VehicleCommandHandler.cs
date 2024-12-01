using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class VehicleCommandHandler(
    IVehicleService vehicleService,
    IVehicleMongoService vehicleMongoService,
    IScheduledRouteService srService,
    IScheduledRouteMongoService srMongoService)
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
        var scheduled = await vehicleService.ReplaceVehicleOnScheduledRoutes(replaceCommand);
        
        var srIds = scheduled.Select(sr => sr.Id).Distinct().ToList();
        var srs = await srService.GetAllByIds(srIds);
        await srMongoService.Update(scheduled);
    }
}
