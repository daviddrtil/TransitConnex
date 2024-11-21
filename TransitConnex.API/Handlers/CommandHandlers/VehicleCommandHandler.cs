using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Vehicle;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class VehicleCommandHandler : IBaseCommandHandler<IVehicleCommand>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        
        public async Task HandleCreate(IVehicleCommand command)
        {
            if (command is VehicleCreateCommand createCommand)
            {
                var newVehicle = await _vehicleService.CreateVehicle(createCommand);
            }
        }

        public async Task HandleUpdate(IVehicleCommand command)
        {
            if (command is VehicleUpdateCommand updateCommand)
            {
                var updatedVehicle =  await _vehicleService.EditVehicle(updateCommand.Id, updateCommand);
            }
        }

        public async Task HandleDelete(IVehicleCommand command)
        {
            if (command is VehicleDeleteCommand deleteCommand)
            {
                await _vehicleService.DeleteVehicle(deleteCommand.Id);
            }
        }
    }
}
