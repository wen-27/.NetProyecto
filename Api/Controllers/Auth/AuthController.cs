using Api.DTOs.Auth;
using Api.Security;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(AppDbContext context, JwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken ct)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var user = await _context.Users
            .Include(x => x.Person)
            .ThenInclude(x => x.Emails)
            .ThenInclude(x => x.EmailDomain)
            .FirstOrDefaultAsync(x => x.IsActive && x.Person.Emails.Any(email =>
                (email.EmailUser + "@" + email.EmailDomain.Domain).ToLower() == normalizedEmail), ct);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Credenciales inválidas." });
        }

        var email = user.Person.Emails
            .OrderByDescending(x => x.IsPrimary)
            .Select(x => $"{x.EmailUser}@{x.EmailDomain.Domain}")
            .FirstOrDefault() ?? normalizedEmail;

        var role = await _context.PersonRoles
            .Where(x => x.PersonId == user.PersonId && x.IsActive)
            .Join(_context.Roles, personRole => personRole.RoleId, role => role.Id, (_, role) => role.RoleName)
            .FirstOrDefaultAsync(ct) ?? "Receptionist";

        var token = _jwtTokenService.CreateToken(user, email, role);

        return Ok(new LoginResponse(user.Id, user.PersonId, email, role, token.Token, token.ExpiresAt));
    }
}
