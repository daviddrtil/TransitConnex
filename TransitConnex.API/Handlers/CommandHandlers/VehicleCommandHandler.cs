using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Application.Commands.Vehicle;
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
            throw new NotImplementedException();
        }

        public async Task HandleDelete(IVehicleCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
