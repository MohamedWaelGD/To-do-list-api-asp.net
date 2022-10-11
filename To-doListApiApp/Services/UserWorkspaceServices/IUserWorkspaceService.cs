using To_doListApiApp.Dtos.UserWorkspaceDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.UserWorkspaceServices
{
    public interface IUserWorkspaceService
    {
        Task<ResponseAPI<List<UserWorkspaceGetDto>>> CreateUserWorkspace(UserWorkspaceCreateDto userWorkspaceCreateDto);
        Task<ResponseAPI<List<UserWorkspaceGetDto>>> GetUserWorkspace();
        Task<ResponseAPI<List<UserWorkspaceGetDto>>> GetUserWorkspace(int workspaceId);
        Task<ResponseAPI<UserWorkspaceGetDto>> EditUserWorkspace(UserWorkspaceEditDto userWorkspaceEditDto);
    }
}
