using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class VehicleMongoService(
    IVehicleMongoRepository vehicleRepo,
    IMapper mapper) : IVehicleMongoService
{
    public async Task<IEnumerable<Vehicle>> GetAll()
    {
        var vehicles = await vehicleRepo.GetAll();
        return mapper.Map<IEnumerable<Vehicle>>(vehicles);
    }

    public async Task<Vehicle?> GetById(Guid id)
    {
        var vehicle = await vehicleRepo.GetById(id);
        if (vehicle == null)
        {
            return null;
        }

        return mapper.Map<Vehicle>(vehicle);
    }

    public async Task<Guid> Create(Vehicle vehicle)
    {
        if (vehicle.Id == Guid.Empty)
        {
            vehicle.Id = Guid.NewGuid(); // Always only add
        }

        var vehicleDoc = mapper.Map<VehicleDoc>(vehicle);
        await vehicleRepo.Upsert(vehicleDoc);
        return vehicle.Id;
    }

    public async Task Update(Vehicle vehicle)
    {
        var vehicleDoc = await vehicleRepo.GetById(vehicle.Id);
        if (vehicleDoc == null)
        {
            return; // Document not exists, update is not performed
        }

        var newVehicleDoc = mapper.Map<VehicleDoc>(vehicle);
        await vehicleRepo.Upsert(newVehicleDoc);
    }

    public async Task Delete(Guid id)
    {
        await vehicleRepo.Delete(id);
    }
}
