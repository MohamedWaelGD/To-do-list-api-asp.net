using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.AuthServices;

namespace To_doListApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseAPI<int>>> Register(UserRegisterDto userRegisterDto)
        {
            var response = await _authService.Register(userRegisterDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseAPI<int>>> Login(UserLoginDto userLoginDto)
        {
            var response = await _authService.Login(userLoginDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
