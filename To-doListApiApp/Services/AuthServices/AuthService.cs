using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_doListApiApp.Data;
using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private static int activeUser = 0;

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Login(UserLoginDto userLoginDto)
        {
            var response = new ResponseAPI<string>();

            if (!await IsUserExists(userLoginDto.Email))
            {
                response.isSuccess = false;
                response.message = "User is not found.";
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == userLoginDto.Email);

            if (!VerifyHashPassword(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.isSuccess = false;
                response.message = "Wrong Password.";
            }

            activeUser = user.Id;
            response.data = user.Email;

            return response;
        }

        public async Task<ResponseAPI<int>> Register(UserRegisterDto userRegisterDto)
        {
            var response = new ResponseAPI<int>();

            if (await IsUserExists(userRegisterDto.Email))
            {
                response.isSuccess = false;
                response.message = "User already signed up in system.";
            }

            var newUser = _mapper.Map<User>(userRegisterDto);

            CreateHashPassword(userRegisterDto.Password, out var passwordHash, out var passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            response.data = newUser.Id;

            return response;
        }

        private async Task<bool> IsUserExists(string email)
        {
            return await _dbContext.Users.AnyAsync(e => e.Email == email);
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyHashPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<bool> IsHasPermission(int workspaceId)
        {
            var userWorkspace = await _dbContext.UserWorkspaces.FirstOrDefaultAsync(e => e.UserId == GetUserId() && e.WorkspaceId == workspaceId);

            return userWorkspace.Role != UserWorkspaceRole.Visitor;
        }

        public int GetUserId()
        {
            return activeUser;
        }
    }
}
