using Microsoft.EntityFrameworkCore;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Vehicle;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<VehicleDto>> GetAllVehicles()
        {
            return await _vehicleRepository.QueryAll().ToDto().ToListAsync();
        }

        public async Task<VehicleDto> GetVehicleById(Guid id) // TODO -> mby we can reutrn null? -> or based on input prop ? -> throwErrorNotFound boolean?
        {
            return await _vehicleRepository.QueryById(id).ToDto().FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
            // return (await _vehicleRepository.QueryById(id).ToDto().FirstOrDefaultAsync())!;
        }

        public async Task<bool> VehicleExists(Guid id)
        {
            return await _vehicleRepository.QueryById(id).AnyAsync();
        }

        public async Task<Vehicle> CreateVehicle(VehicleCreateCommand vehicleDto)
        {
            var newVehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Label = vehicleDto.Label,
                Spz = vehicleDto.Spz,
                Manufacturer = vehicleDto.Manufacturer,
                Capacity = vehicleDto.Capacity,
                VehicleType = vehicleDto.VehicleType,
                IconId = vehicleDto.IconId,
                LineId = vehicleDto.LineId
            };
            
            await _vehicleRepository.Add(newVehicle);

            return newVehicle;
        }

        public async Task<Vehicle> EditVehicle(Guid id, VehicleUpdateCommand editedVehicle)
        {
            var vehicle = await _vehicleRepository.QueryById(id).FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
            }
            
            // TOODO -> think of better way to do this? -> mby reflection?
            // vehicle.Manufacturer = editedVehicle.Manufacturer;
            // vehicle.Capacity = editedVehicle.Capacity;
            // vehicle.Spz = editedVehicle.Spz;
            // vehicle.VehicleType = editedVehicle.VehicleType;
            // vehicle.IconId = editedVehicle.IconId;
            // vehicle.LineId = editedVehicle.LineId;
            
            await _vehicleRepository.Update(vehicle, editedVehicle);
            
            return vehicle;
        }

        public async Task DeleteVehicle(Guid id)
        {
            var vehicle = await _vehicleRepository.QueryById(id).FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {id} was not found.");
            }
            
            await _vehicleRepository.Delete(vehicle);
        }
    }
}
