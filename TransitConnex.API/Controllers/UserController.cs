using Microsoft.AspNetCore.Mvc;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        // TODO -> endpoints
        
    }
}
