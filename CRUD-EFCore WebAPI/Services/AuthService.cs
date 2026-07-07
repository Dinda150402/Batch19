using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using CRUDEFCore.Common;
using CRUDEFCore.DTOs;
using CRUDEFCore.Models;
using CRUDEFCore.Repositories;

namespace CRUDEFCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthService(
            IUserRepository userRepo,
            IConfiguration config,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _userRepo = userRepo;
            _config = config;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
        {
            var validation = await _registerValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult.Fail("Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var existing = await _userRepo.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return ServiceResult.Fail("Username sudah dipakai.");

            var user = new User { Username = dto.Username, Role = dto.Role };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return ServiceResult.Ok("Registrasi berhasil.");
        }

        public async Task<ServiceResult<string>> LoginAsync(LoginDto dto)
        {
            var validation = await _loginValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult<string>.Fail("Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var user = await _userRepo.GetByUsernameAsync(dto.Username);
            if (user == null)
                return ServiceResult<string>.Fail("Username atau password salah.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return ServiceResult<string>.Fail("Username atau password salah.");

            string token = GenerateJwtToken(user);
            return ServiceResult<string>.Ok(token, "Login berhasil.");
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
