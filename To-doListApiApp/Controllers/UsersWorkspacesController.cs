using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_doListApiApp.Dtos.UserWorkspaceDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.UserWorkspaceServices;

namespace To_doListApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersWorkspacesController : ControllerBase
    {
        private readonly IUserWorkspaceService _userWorkspaceService;

        public UsersWorkspacesController(IUserWorkspaceService userWorkspaceService)
        {
            this._userWorkspaceService = userWorkspaceService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI<List<UserWorkspaceGetDto>>>> CreateUserWorkspace(UserWorkspaceCreateDto userWorkspaceCreateDto)
        {
            var response = await _userWorkspaceService.CreateUserWorkspace(userWorkspaceCreateDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI<UserWorkspaceGetDto>>> EditUserWorkspace(UserWorkspaceEditDto userWorkspaceEditDto)
        {
            var response = await _userWorkspaceService.EditUserWorkspace(userWorkspaceEditDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<UserWorkspaceGetDto>>>> GetUserWorkspace()
        {
            var response = await _userWorkspaceService.GetUserWorkspace();

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI<List<UserWorkspaceGetDto>>>> GetUserWorkspace(int id)
        {
            var response = await _userWorkspaceService.GetUserWorkspace(id);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
