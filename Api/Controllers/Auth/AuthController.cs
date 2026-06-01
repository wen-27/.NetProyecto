using Api.DTOs.Auth;
using Api.Security;
using Domain.Entities;
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
    private const string ClientRoleName = "Client";
    private static readonly HashSet<string> DevelopmentSeedPasswords = new(StringComparer.Ordinal)
    {
        "DevPass123!",
        "Password123*"
    };

    private readonly AppDbContext _context;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IWebHostEnvironment _environment;

    public AuthController(AppDbContext context, JwtTokenService jwtTokenService, IWebHostEnvironment environment)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _environment = environment;
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

        if (user is null)
        {
            return Unauthorized(new { message = "Credenciales inválidas." });
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!passwordIsValid && CanRepairDevelopmentSeedPassword(normalizedEmail, request.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.IsActive = true;
            await _context.SaveChangesAsync(ct);
            passwordIsValid = true;
        }

        if (!passwordIsValid)
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

    private bool CanRepairDevelopmentSeedPassword(string normalizedEmail, string password)
    {
        if (!_environment.IsDevelopment() || !DevelopmentSeedPasswords.Contains(password))
        {
            return false;
        }

        return normalizedEmail.EndsWith("@autotaller.com", StringComparison.OrdinalIgnoreCase) ||
            normalizedEmail is "carlos.ramirez@test.com" or "laura.gomez@test.com" or "client@mail.com" or "admin@mail.com" or "mechanic@mail.com" or "receptionist@mail.com";
    }

    [HttpPost("register-client")]
    public async Task<ActionResult<LoginResponse>> RegisterClient(RegisterClientRequest request, CancellationToken ct)
    {
        var normalizedEmail = NormalizeEmail(request.Email);
        var emailParts = SplitEmail(normalizedEmail);
        if (emailParts is null)
        {
            return BadRequest(new { message = "El correo electrónico no tiene un formato válido." });
        }

        if (string.IsNullOrWhiteSpace(request.DocumentNumber) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName))
        {
            return BadRequest(new { message = "Documento, nombre y apellido son obligatorios." });
        }

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
        {
            return BadRequest(new { message = "La contraseña debe tener al menos 8 caracteres." });
        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && !request.PhoneCountryId.HasValue)
        {
            return BadRequest(new { message = "Debe indicar el país del teléfono." });
        }

        var documentExists = await _context.Persons
            .AnyAsync(x => x.DocumentNumber == request.DocumentNumber.Trim(), ct);
        if (documentExists)
        {
            return Conflict(new { message = "Ya existe una persona con ese documento." });
        }

        var emailExists = await _context.PersonEmails
            .Include(x => x.EmailDomain)
            .AnyAsync(x => (x.EmailUser + "@" + x.EmailDomain.Domain).ToLower() == normalizedEmail, ct);
        if (emailExists)
        {
            return Conflict(new { message = "Ya existe un usuario registrado con ese correo." });
        }

        var clientRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == ClientRoleName, ct);
        if (clientRole is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "No existe el rol Client en la base de datos." });
        }

        var documentTypeExists = await _context.DocumentTypes.AnyAsync(x => x.Id == request.DocumentTypeId, ct);
        if (!documentTypeExists)
        {
            return BadRequest(new { message = "El tipo de documento no existe." });
        }

        if (request.GenderId.HasValue && !await _context.Genders.AnyAsync(x => x.Id == request.GenderId.Value, ct))
        {
            return BadRequest(new { message = "El género no existe." });
        }

        if (request.AddressId.HasValue && !await _context.Addresses.AnyAsync(x => x.Id == request.AddressId.Value, ct))
        {
            return BadRequest(new { message = "La dirección no existe." });
        }

        if (request.PhoneCountryId.HasValue && !await _context.Countries.AnyAsync(x => x.Id == request.PhoneCountryId.Value, ct))
        {
            return BadRequest(new { message = "El país del teléfono no existe." });
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);

        var person = new Person
        {
            DocumentTypeId = request.DocumentTypeId,
            DocumentNumber = request.DocumentNumber.Trim(),
            FirstName = request.FirstName.Trim(),
            MiddleName = string.IsNullOrWhiteSpace(request.MiddleName) ? null : request.MiddleName.Trim(),
            LastName = request.LastName.Trim(),
            SecondLastName = string.IsNullOrWhiteSpace(request.SecondLastName) ? null : request.SecondLastName.Trim(),
            BirthDate = request.BirthDate,
            GenderId = request.GenderId,
            AddressId = request.AddressId
        };

        await _context.Persons.AddAsync(person, ct);
        await _context.SaveChangesAsync(ct);

        var domain = await _context.EmailDomains.FirstOrDefaultAsync(x => x.Domain == emailParts.Value.Domain, ct);
        if (domain is null)
        {
            domain = new EmailDomain { Domain = emailParts.Value.Domain };
            await _context.EmailDomains.AddAsync(domain, ct);
            await _context.SaveChangesAsync(ct);
        }

        await _context.PersonEmails.AddAsync(new PersonEmail
        {
            PersonId = person.Id,
            EmailDomainId = domain.Id,
            EmailUser = emailParts.Value.User,
            IsPrimary = true
        }, ct);

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && request.PhoneCountryId.HasValue)
        {
            await _context.PersonPhones.AddAsync(new PersonPhone
            {
                PersonId = person.Id,
                CountryId = request.PhoneCountryId.Value,
                PhoneNumber = request.PhoneNumber.Trim(),
                IsPrimary = true
            }, ct);
        }

        await _context.PersonRoles.AddAsync(new PersonRole
        {
            PersonId = person.Id,
            RoleId = clientRole.Id,
            IsActive = true
        }, ct);

        var user = new User
        {
            PersonId = person.Id,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = true
        };

        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        var token = _jwtTokenService.CreateToken(user, normalizedEmail, clientRole.RoleName);
        return Created("/api/auth/register-client", new LoginResponse(user.Id, person.Id, normalizedEmail, clientRole.RoleName, token.Token, token.ExpiresAt));
    }

    private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

    private static (string User, string Domain)? SplitEmail(string email)
    {
        var parts = email.Split('@', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 || parts[0].Length == 0 || parts[1].Length == 0 || !parts[1].Contains('.'))
        {
            return null;
        }

        return (parts[0], parts[1]);
    }
}
