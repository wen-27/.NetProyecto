using System.Text;
using Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

// Extension que agrupa configuracion de servicios para mantener Program.cs mas claro.
public static class AuthServiceExtensions
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public static IServiceCollection AddJwtAuthService(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
        {
            throw new InvalidOperationException(
                "Jwt:Key must be configured with a value of at least 32 characters. Use user-secrets locally or Jwt__Key in the environment.");
        }

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
            options.AddPolicy("WorkshopChiefOnly", policy => policy.RequireRole("WorkshopChief"));
            options.AddPolicy("WarehouseChiefOnly", policy => policy.RequireRole("WarehouseChief"));
            options.AddPolicy("InventoryManagerOnly", policy => policy.RequireRole("InventoryManager"));
            options.AddPolicy("InternalStaff", policy => policy.RequireRole("Admin", "Mechanic", "Receptionist", "WorkshopChief", "WarehouseChief", "InventoryManager"));
            options.AddPolicy("MechanicOrAdmin", policy => policy.RequireRole("Mechanic", "Admin"));
            options.AddPolicy("ReceptionistOrAdmin", policy => policy.RequireRole("Receptionist", "WorkshopChief", "Admin"));
            options.AddPolicy("WorkshopChiefOrAdmin", policy => policy.RequireRole("WorkshopChief", "Admin"));
            options.AddPolicy("WarehouseChiefOrAdmin", policy => policy.RequireRole("WarehouseChief", "Admin"));
            options.AddPolicy("InventoryManagerOrAdmin", policy => policy.RequireRole("InventoryManager", "Admin"));
            options.AddPolicy("ReceptionistMechanicOrAdmin", policy => policy.RequireRole("Receptionist", "Mechanic", "Admin"));
        });

        services.AddScoped<JwtTokenService>();

        return services;
    }
}
