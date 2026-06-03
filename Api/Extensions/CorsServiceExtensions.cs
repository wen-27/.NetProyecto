namespace Api.Extensions;

// Extension que agrupa configuracion de servicios para mantener Program.cs mas claro.
public static class CorsServiceExtensions
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public const string PolicyName = "AutoTallerCors";

    public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, policy =>
            {
                if (allowedOrigins.Length > 0)
                {
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();

                    return;
                }

                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}
