using AutoMapper;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.DTOs.VehicleRTI;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Services;

public class VehicleRTIMongoService(
    IVehicleRTIMongoRepository vehicleRTIRepo,
    IMapper mapper) : IVehicleRTIMongoService
{
    public async Task<IEnumerable<VehicleRTIDto>> GetAll()
    {
        var vehicleRTIDocs = await vehicleRTIRepo.GetAll();
        return mapper.Map<IEnumerable<VehicleRTIDto>>(vehicleRTIDocs);
    }

    public async Task<VehicleRTIDto?> GetById(Guid vehicleId)
    {
        var vehicleRTIDoc = await vehicleRTIRepo.GetByVehicleId(vehicleId);
        if (vehicleRTIDoc == null)
        {
            return null;
        }

        return mapper.Map<VehicleRTIDto>(vehicleRTIDoc);
    }

    public async Task<Guid> Create(VehicleRTIDto vehicleRTI)
    {
        if (vehicleRTI.Id == Guid.Empty)
        {
            vehicleRTI.Id = Guid.NewGuid(); // Always only add
        }

        var vehicleRTIDoc = mapper.Map<VehicleRTIDoc>(vehicleRTI);
        await vehicleRTIRepo.Upsert(vehicleRTIDoc);
        return vehicleRTI.Id;
    }
}
