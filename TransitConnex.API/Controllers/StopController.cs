using Microsoft.AspNetCore.Mvc;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopController : Controller
    {
        private readonly IStopService _stopService;

        public StopController(IStopService stopService)
        {
            _stopService = stopService;
        }
    }
}
