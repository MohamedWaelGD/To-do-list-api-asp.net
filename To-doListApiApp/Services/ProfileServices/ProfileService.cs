using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_doListApiApp.Data;
using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.AuthServices;

namespace To_doListApiApp.Services.ProfileServices
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ProfileService(AppDbContext dbContext, IMapper mapper, IAuthService authService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._authService = authService;
        }

        public async Task<ResponseAPI<UserGetDto>> GetProfile()
        {
            var response = new ResponseAPI<UserGetDto>();

            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == _authService.GetUserId());

            response.data = _mapper.Map<UserGetDto>(user);

            return response;
        }
    }
}
