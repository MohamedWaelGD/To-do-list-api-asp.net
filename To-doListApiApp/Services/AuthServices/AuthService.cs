using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using To_doListApiApp.Data;
using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
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

            response.data = CreateToken(user);

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

        public async Task<bool> IsWorkspaceOwner(int workspaceId)
        {
            var userWorkspace = await _dbContext.UserWorkspaces.FirstOrDefaultAsync(e => e.UserId == GetUserId() && e.WorkspaceId == workspaceId);

            return userWorkspace.Role == UserWorkspaceRole.Owner;
        }

        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
