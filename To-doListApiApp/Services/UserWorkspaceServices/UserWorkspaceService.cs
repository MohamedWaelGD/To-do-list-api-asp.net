using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_doListApiApp.Data;
using To_doListApiApp.Dtos.UserWorkspaceDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.AuthServices;

namespace To_doListApiApp.Services.UserWorkspaceServices
{
    public class UserWorkspaceService : IUserWorkspaceService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserWorkspaceService(AppDbContext dbContext, IMapper mapper, IAuthService authService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._authService = authService;
        }

        public async Task<ResponseAPI<List<UserWorkspaceGetDto>>> CreateUserWorkspace(UserWorkspaceCreateDto userWorkspaceCreateDto)
        {
            var response = new ResponseAPI<List<UserWorkspaceGetDto>>();
            if (!await IsUserExists(userWorkspaceCreateDto.UserId))
            {
                response.isSuccess = false;
                response.message = "User not found.";
                return response;
            }

            if (!await IsWorkspaceExists(userWorkspaceCreateDto.WorkspaceId))
            {
                response.isSuccess = false;
                response.message = "Workspace not found.";
                return response;
            }

            var userWorkspace = _mapper.Map<UserWorkspace>(userWorkspaceCreateDto);
            await _dbContext.UserWorkspaces.AddAsync(userWorkspace);
            await _dbContext.SaveChangesAsync();

            return await GetUserWorkspace();
        }

        public async Task<ResponseAPI<UserWorkspaceGetDto>> EditUserWorkspace(UserWorkspaceEditDto userWorkspaceEditDto)
        {
            var response = new ResponseAPI<UserWorkspaceGetDto>();
            if (!await IsUserExists(userWorkspaceEditDto.UserId))
            {
                response.isSuccess = false;
                response.message = "User not found.";
                return response;
            }

            if (!await IsWorkspaceExists(userWorkspaceEditDto.WorkspaceId))
            {
                response.isSuccess = false;
                response.message = "Workspace not found.";
                return response;
            }

            var userWorkspace = await _dbContext.UserWorkspaces
                .FirstOrDefaultAsync(e => e.UserId == userWorkspaceEditDto.UserId && e.WorkspaceId == userWorkspaceEditDto.WorkspaceId);

            if (userWorkspace == null)
            {
                response.isSuccess = false;
                response.message = "User or workspace not found.";
                return response;
            }

            _mapper.Map(userWorkspaceEditDto, userWorkspace);

            await _dbContext.SaveChangesAsync();

            response.data = _mapper.Map<UserWorkspaceGetDto>(userWorkspace);

            return response;
        }

        public async Task<ResponseAPI<List<UserWorkspaceGetDto>>> GetUserWorkspace()
        {
            var response = new ResponseAPI<List<UserWorkspaceGetDto>>();

            response.data = await _dbContext.UserWorkspaces
                .Include(e => e.Workspace)
                .Include(e => e.User)
                .Where(e => e.UserId == _authService.GetUserId())
                .Select(e => _mapper.Map<UserWorkspaceGetDto>(e))
                .ToListAsync();

            return response;
        }

        private async Task<bool> IsUserExists(int id)
        {
            return await _dbContext.Users.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> IsWorkspaceExists(int id)
        {
            return await _dbContext.Workspaces.AnyAsync(e => e.Id == id);
        }
    }
}
