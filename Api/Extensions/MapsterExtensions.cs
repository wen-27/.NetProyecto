// Responsabilidad: Extension de configuracion usada para mantener Program.cs legible y centralizar registro de servicios o politicas de la API.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using System.Reflection;
using Mapster;

namespace Api.Extensions;

public static class MapsterExtensions
{
    public static IServiceCollection AddMapsterService(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);

        return services;
    }
}
