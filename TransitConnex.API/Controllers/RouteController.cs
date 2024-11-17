using Microsoft.AspNetCore.Mvc;
using TransitConnex.Infrastructure.Repositories.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : Controller
    {
        private readonly IRouteRepository _routeRepository;

        public RouteController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
    }
}
