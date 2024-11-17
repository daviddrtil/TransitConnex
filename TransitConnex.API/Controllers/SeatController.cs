using Microsoft.AspNetCore.Mvc;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : Controller
    {
        private readonly ISeatRepository _seatRepository;

        public SeatController(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
    }
}
