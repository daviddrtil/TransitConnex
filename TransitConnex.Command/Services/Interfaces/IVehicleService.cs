using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Vehicle;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetAllVehicles();

        Task<VehicleDto> GetVehicleById(Guid id);

        Task<bool> VehicleExists(Guid id);

        Task<Vehicle> CreateVehicle(VehicleCreateCommand vehicleDto);

        Task<Vehicle> EditVehicle(Guid id, VehicleUpdateCommand editedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
