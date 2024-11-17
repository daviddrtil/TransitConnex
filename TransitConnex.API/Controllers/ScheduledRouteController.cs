using Microsoft.AspNetCore.Mvc;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledRouteController : Controller
    {
        private readonly IScheduledRouteRepository _scheduledRouteRepository;

        public ScheduledRouteController(IScheduledRouteRepository scheduledRouteRepository)
        {
            _scheduledRouteRepository = scheduledRouteRepository;
        }
        
        
    }
}
