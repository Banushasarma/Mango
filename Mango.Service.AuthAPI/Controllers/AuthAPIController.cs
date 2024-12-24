using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
