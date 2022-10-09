using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_doListApiApp.Data;
using To_doListApiApp.Dtos.WorkspaceDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.AuthServices;

namespace To_doListApiApp.Services.WorkspaceServices
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public WorkspaceService(AppDbContext dbContext, IMapper mapper, IAuthService authService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._authService = authService;
        }

        public async Task<ResponseAPI<IEnumerable<WorkspaceGetDto>>> CreateWorkspace(WorkspaceCreateDto workspaceCreateDto)
        {
            var workspace = _mapper.Map<Workspace>(workspaceCreateDto);

            await _dbContext.Workspaces.AddAsync(workspace);
            await _dbContext.SaveChangesAsync();

            var userWorkspace = new UserWorkspace
            {
                UserId = _authService.GetUserId(),
                WorkspaceId = workspace.Id
            };

            await _dbContext.UserWorkspaces.AddAsync(userWorkspace);
            await _dbContext.SaveChangesAsync();

            return await GetWorkspaces();
        }

        public async Task<ResponseAPI<IEnumerable<WorkspaceGetDto>>> DeleteWorkspace(int id)
        {
            var response = new ResponseAPI<IEnumerable<WorkspaceGetDto>>();
            if (!await IsWorkspaceExists(id))
            {
                response.isSuccess = false;
                response.message = "Workspace is not found.";
                return response;
            }

            if (!await _authService.IsHasPermission(id))
            {
                response.isSuccess = false;
                response.message = "User does not has permission.";
                return response;
            }

            var workspace = await _dbContext.Workspaces.FirstOrDefaultAsync(e => e.Id == id);
            _dbContext.Workspaces.Remove(workspace);

            await _dbContext.SaveChangesAsync();

            return await GetWorkspaces();
        }

        public async Task<ResponseAPI<WorkspaceGetDto>> EditWorkspace(WorkspaceEditDto workspaceEditDto)
        {
            var response = new ResponseAPI<WorkspaceGetDto>();
            if (!await IsWorkspaceExists(workspaceEditDto.Id))
            {
                response.isSuccess = false;
                response.message = "Workspace is not found.";
                return response;
            }

            if (!await _authService.IsHasPermission(workspaceEditDto.Id))
            {
                response.isSuccess = false;
                response.message = "User does not has permission.";
                return response;
            }


            var workspace = await _dbContext.Workspaces.FirstOrDefaultAsync(e => e.Id == workspaceEditDto.Id);
            _mapper.Map(workspaceEditDto, workspace);
            await _dbContext.SaveChangesAsync();

            response.data = _mapper.Map<WorkspaceGetDto>(workspace);
            return response;
        }

        public async Task<ResponseAPI<IEnumerable<WorkspaceGetDto>>> GetWorkspaces()
        {
            var response = new ResponseAPI<IEnumerable<WorkspaceGetDto>>();

            response.data = await _dbContext.UserWorkspaces
                .Include(e => e.Workspace)
                .Where(e => e.UserId == _authService.GetUserId())
                .Select(e => _mapper.Map<WorkspaceGetDto>(e.Workspace))
                .ToListAsync();

            return response;
        }

        private async Task<bool> IsWorkspaceExists(int id)
        {
            return await _dbContext.Workspaces.AnyAsync(e => e.Id == id);
        }
    }
}
