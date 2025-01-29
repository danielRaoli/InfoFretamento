using InfoFretamento.Application.Request.Auth;
using InfoFretamento.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoFretamento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(AuthService service) : ControllerBase
    {
        private readonly AuthService _userService = service;

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var response = _userService.Login(loginRequest);

                return Ok(response);
            }
            catch(Exception)
            {
                return Unauthorized();
            }

        }

        [HttpGet]
        public async Task<IActionResult> HelfCheck()
        {
            return Ok("Hello World");
        }
    }
}
