using InfoFretamento.Application.Request.Auth;
using InfoFretamento.Application.Services;


using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(AuthService service) : ControllerBase
    {
        private readonly AuthService _userService = service;

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var response = await _userService.Login(loginRequest);

                return Ok(response);
            }
            catch(Exception)
            {
                return Unauthorized();
            }

        }
    }
}
