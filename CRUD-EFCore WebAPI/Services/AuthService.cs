using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using CRUDEFCore.Common;
using CRUDEFCore.DTOs;
using CRUDEFCore.Models;

namespace CRUDEFCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
        {
            var validation = await _registerValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult.Fail("Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var existing = await _userManager.FindByNameAsync(dto.Username);
            if (existing != null)
                return ServiceResult.Fail("Username sudah dipakai.");

            var user = new ApplicationUser { UserName = dto.Username };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return ServiceResult.Fail("Registrasi gagal.", result.Errors.Select(e => e.Description).ToList());

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));

            await _userManager.AddToRoleAsync(user, dto.Role);

            return ServiceResult.Ok("Registrasi berhasil.");
        }

        public async Task<ServiceResult<string>> LoginAsync(LoginDto dto)
        {
            var validation = await _loginValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult<string>.Fail("Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
                return ServiceResult<string>.Fail("Username atau password salah.");

            bool passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid)
                return ServiceResult<string>.Fail("Username atau password salah.");

            var roles = await _userManager.GetRolesAsync(user);
            string token = GenerateJwtToken(user, roles);
            return ServiceResult<string>.Ok(token, "Login berhasil.");
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id) 
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtKey = _config["Jwt:Key"] ?? "FallbackDevKey_MinimumLength32Characters_ForBootcamp!";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
