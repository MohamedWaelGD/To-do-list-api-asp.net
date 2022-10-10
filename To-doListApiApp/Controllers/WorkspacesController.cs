using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_doListApiApp.Dtos.WorkspaceDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.WorkspaceServices;

namespace To_doListApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspacesController(IWorkspaceService workspaceService)
        {
            this._workspaceService = workspaceService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI<IEnumerable<WorkspaceGetDto>>>> CreateWorkspace(WorkspaceCreateDto workspaceCreateDto)
        {
            var response = await _workspaceService.CreateWorkspace(workspaceCreateDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<IEnumerable<WorkspaceGetDto>>>> GetWorkspaces()
        {
            var response = await _workspaceService.GetWorkspaces();

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI<WorkspaceGetDto>>> EditWorkspace(WorkspaceEditDto workspaceEditDto)
        {
            var response = await _workspaceService.EditWorkspace(workspaceEditDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{workspaceId}")]
        public async Task<ActionResult<ResponseAPI<IEnumerable<WorkspaceGetDto>>>> DeleteWorkspace(int workspaceId)
        {
            var response = await _workspaceService.DeleteWorkspace(workspaceId);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
