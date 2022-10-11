using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ResponseAPI<int>> Register(UserRegisterDto userRegisterDto);
        Task<ResponseAPI<string>> Login(UserLoginDto userLoginDto);
        Task<bool> IsHasPermission(int workspaceId);
        Task<bool> IsWorkspaceOwner(int workspaceId);
        Task<bool> IsOwner(int workspaceId);
        int GetUserId();
    }
}
