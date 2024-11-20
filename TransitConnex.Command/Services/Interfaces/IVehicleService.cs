using TransitConnex.Application.Commands.Vehicle;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetAllVehicles();

        Task<VehicleDto> GetVehicleById(Guid id);

        Task<bool> VehicleExists(Guid id);

        Task<Vehicle> CreateVehicle(VehicleCreateCommand vehicleDto);

        Task<VehicleDto> EditVehicle(Guid id, VehicleCreateDto editedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
