using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.ProfileServices
{
    public interface IProfileService
    {
        Task<ResponseAPI<UserGetDto>> GetProfile();
    }
}
