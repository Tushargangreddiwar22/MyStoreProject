using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(
            IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult>  Register(RegisterDto dto)
        {
            await _service.RegisterAsync(dto);

            return Ok(
                "User Registered Successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult>  Login(LoginDto dto)
        {
            var result =
                await _service.LoginAsync(dto);

            return Ok(result);
        }
    }
}
