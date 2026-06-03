using Microsoft.OpenApi;

namespace Api.Extensions;

// Extension que agrupa configuracion de servicios para mantener Program.cs mas claro.
public static class SwaggerServiceExtensions
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", document, null),
                    new List<string>()
                }
            });
        });

        return services;
    }
}
