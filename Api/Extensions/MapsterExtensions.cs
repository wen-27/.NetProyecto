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
