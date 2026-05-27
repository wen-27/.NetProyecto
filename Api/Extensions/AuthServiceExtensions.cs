using System.Text;
using Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class AuthServiceExtensions
{
    public static IServiceCollection AddJwtAuthService(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"]!;

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("MechanicOnly", policy => policy.RequireRole("Mechanic"));
            options.AddPolicy("ReceptionistOnly", policy => policy.RequireRole("Receptionist"));
            options.AddPolicy("ClientOnly", policy => policy.RequireRole("Client"));
            options.AddPolicy("InternalStaff", policy => policy.RequireRole("Admin", "Mechanic", "Receptionist"));
            options.AddPolicy("MechanicOrAdmin", policy => policy.RequireRole("Mechanic", "Admin"));
            options.AddPolicy("ReceptionistOrAdmin", policy => policy.RequireRole("Receptionist", "Admin"));
            options.AddPolicy("ReceptionistMechanicOrAdmin", policy => policy.RequireRole("Receptionist", "Mechanic", "Admin"));
        });

        services.AddScoped<JwtTokenService>();

        return services;
    }
}
