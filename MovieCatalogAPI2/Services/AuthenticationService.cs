using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieCatalogAPI2.Models;

namespace MovieCatalogAPI2.Services
{
    public class AuthenticationService
    {
        private readonly IConfiguration _config;

        public AuthenticationService(IConfiguration config)
        {
            _config = config;
        }

        // Hash password using BCrypt
        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string hash, string password) =>
            BCrypt.Net.BCrypt.Verify(password, hash);

        // Generate JWT using settings from appsettings.json
        public string CreateJwtToken(User user)
        {
            var jwtSection = _config.GetSection("Jwt");
            var keyString = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key not configured");
            var issuer = jwtSection["Issuer"] ?? "";
            var audience = jwtSection["Audience"] ?? "";
            var expiresMinutes = int.TryParse(jwtSection["ExpiresMinutes"], out var m) ? m : 60;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username ?? user.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
