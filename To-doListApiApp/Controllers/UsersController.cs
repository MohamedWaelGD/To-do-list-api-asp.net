using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.ProfileServices;

namespace To_doListApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public UsersController(IProfileService profileService)
        {
            this._profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<UserGetDto>>> GetProfile()
        {
            var response = await _profileService.GetProfile();

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI<UserGetDto>>> EditProfile(UserEditDto userEditDto)
        {
            var response = await _profileService.EditProfile(userEditDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
