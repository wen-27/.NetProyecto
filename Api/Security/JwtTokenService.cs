using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Api.Security;

// Componente de seguridad usado por autenticacion/autorizacion dentro de la API.
public sealed class JwtTokenService
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime ExpiresAt) CreateToken(User user, string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;
        var key = _configuration["Jwt:Key"]!;
        var expiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiresMinutes"] ?? "120"));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim("PersonId", user.PersonId.ToString())
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
