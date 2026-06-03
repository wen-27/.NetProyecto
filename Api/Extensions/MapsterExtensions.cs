using System.Reflection;
using Mapster;

namespace Api.Extensions;

// Extension que agrupa configuracion de servicios para mantener Program.cs mas claro.
public static class MapsterExtensions
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public static IServiceCollection AddMapsterService(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);

        return services;
    }
}
