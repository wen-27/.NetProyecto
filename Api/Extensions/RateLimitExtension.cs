// Extension de configuracion usada para mantener Program.cs legible y centralizar registro de servicios o politicas de la API.
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Extensions;

// Extension que agrupa configuracion de servicios para mantener Program.cs mas claro.
public static class RateLimitExtension
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public static IServiceCollection AddRateLimitService(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddFixedWindowLimiter("service-orders", limiter =>
            {
                limiter.PermitLimit = 15;
                limiter.Window = TimeSpan.FromMinutes(1);
                limiter.QueueLimit = 0;
                limiter.AutoReplenishment = true;
            });

            options.AddFixedWindowLimiter("parts", limiter =>
            {
                limiter.PermitLimit = 15;
                limiter.Window = TimeSpan.FromMinutes(1);
                limiter.QueueLimit = 0;
                limiter.AutoReplenishment = true;
            });

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 15,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    }));
        });

        return services;
    }
}
