using Application.Abstractions;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.VehicleOwnerHistory;
using Infrastructure.Repositories.Vehicles;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySql")!;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleOwnerHistoryRepository, VehicleOwnerHistoryRepository>();
        services.AddRepositoryProxies();

        return services;
    }

    private static IServiceCollection AddRepositoryProxies(this IServiceCollection services)
    {
        var repositoryTypes = typeof(IUnitOfWork).Assembly
            .GetTypes()
            .Where(x => x.IsInterface && x.Namespace == "Application.Abstractions" && x.Name.StartsWith('I') && x.Name.EndsWith("Repository"))
            .ToArray();

        foreach (var repositoryType in repositoryTypes)
        {
            if (services.Any(x => x.ServiceType == repositoryType))
            {
                continue;
            }

            services.AddScoped(repositoryType, provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                return EfRepositoryProxy.Create(repositoryType, context);
            });
        }

        return services;
    }
}
