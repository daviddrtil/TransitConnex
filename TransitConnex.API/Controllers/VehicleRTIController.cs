using Microsoft.AspNetCore.Mvc;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleRTIController(IVehicleRTIRepository vehicleRTIRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<VehicleRTIDoc>> GetAll()
        {
            return await vehicleRTIRepo.GetAllAsync();
        }

        [HttpGet("GetByVehicleId/{id}")]
        public async Task<VehicleRTIDoc> GetByVehicleId(Guid id)
        {
            return await vehicleRTIRepo.GetByVehicleIdAsync(id);
            //return vehicle is null ? Ok(vehicle) : NotFound();
        }

        // todo rename endpoint
        [HttpPost]
        public async Task AddVehicleRTI(VehicleRTIDoc vehicleRTI)
        {
            if (vehicleRTI.Id == Guid.Empty)
                vehicleRTI.Id = Guid.NewGuid(); // Always only add
            await vehicleRTIRepo.UpsertAsync(vehicleRTI);
        }
    }
}
