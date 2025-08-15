using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Minimal.Application.Interfaces.Services;
using Minimal.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Minimal.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SymmetricSecurityKey _jwtKey;
    private readonly string? _issuer;
    private readonly string? _audience;
    private readonly double _expireHours;

    public AuthenticationService(IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave JWT (Jwt:Key) não configurada.");
        _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
        _expireHours = configuration.GetValue<double>("Jwt:ExpireHours");
    }

    public string GenerateJwtToken(Administrator administrator)
    {
        var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, administrator.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, administrator.Email),
            new(ClaimTypes.Role, administrator.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_expireHours),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}