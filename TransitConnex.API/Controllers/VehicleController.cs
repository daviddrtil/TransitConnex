using Microsoft.AspNetCore.Mvc;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        // private readonly AppDbContext _context;
        private readonly IVehicleService _vehicleService;

        public VehicleController(AppDbContext context, IVehicleService vehicleService)
        {
            // _context = context;
            _vehicleService = vehicleService;
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            return await _vehicleService.GetAllVehicles();
        }

        // GET: api/Vehicle/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(Guid id)
        {
            if (!await _vehicleService.VehicleExists(id))
            {
                return NotFound($"Vehicle with id: {id} was not found.");
            }
            
            return await _vehicleService.GetVehicleById(id);
        }

        // POST: api/Vehicle
        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(VehicleCreateDto vehicle)
        {
            return await _vehicleService.CreateVehicle(vehicle);
        }
        
        // // PUT: api/Vehicle/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(Guid id, VehicleCreateDto editedVehicle)
        {
            if (!await _vehicleService.VehicleExists(id))
            {
                return NotFound($"Vehicle with id: {id} was not found.");
            }
        
            await _vehicleService.EditVehicle(id, editedVehicle);
        
            return NoContent();
        }
        
        // // DELETE: api/Vehicle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            if (!await _vehicleService.VehicleExists(id))
            {
                return NotFound($"Vehicle with ID {id} was not found.");
            }

            await _vehicleService.DeleteVehicle(id);
        
            return NoContent();
        }
    }
}
