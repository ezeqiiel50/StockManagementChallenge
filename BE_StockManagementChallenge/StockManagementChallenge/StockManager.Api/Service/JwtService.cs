using Microsoft.IdentityModel.Tokens;
using ROP;
using StockManager.Application.DTOs.Login;
using StockManager.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManager.Api.Service
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public async Task<Result<string>> GenerateToken(UserData user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return await new JwtSecurityTokenHandler().WriteToken(token).Success().Async();
        }
    }
}
