using TransitConnex.Domain.DTOs.Vehicle;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetAllVehicles();

        Task<VehicleDto> GetVehicleById(Guid id);

        Task<bool> VehicleExists(Guid id);

        Task<VehicleDto> CreateVehicle(VehicleCreateDto vehicleDto);

        Task<VehicleDto> EditVehicle(Guid id, VehicleCreateDto editedVehicle);

        Task DeleteVehicle(Guid id);
    }
}
