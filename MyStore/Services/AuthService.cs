using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyStore.DTOs;
using MyStore.Entities;
using StoreManagement.API.Repositories.Interfaces;

namespace MyStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(
            IUserRepository repo,
            IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var existingUser =
                await _repo.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true,
                RoleId = 1
            };

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user =
                await _repo.GetByEmailAsync(
                    dto.Email);

            if (user == null)
                throw new Exception(
                    "Invalid Email");

            bool valid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!valid)
                throw new Exception(
                    "Invalid Password");

            var token =
                GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role?.Name ?? string.Empty
            };
        }

        private string GenerateJwtToken(
            User user)
        {
            var email = user.Email ?? throw new InvalidOperationException("User email is null.");
            var roleName = user.Role?.Name ?? string.Empty;

            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException("JWT key is not configured.");

            var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT issuer is not configured.");
            var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT audience is not configured.");

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Email,
                    email),

                new Claim(ClaimTypes.Role, roleName)
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtKey));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires:
                        DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
