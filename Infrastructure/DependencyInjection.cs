using Application.Abstractions;
using Application.Abstractions.OperationalWorkflow;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        services.AddScoped<IOperationalWorkflowService, OperationalWorkflowService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddConcreteRepositories();

        return services;
    }

    private static IServiceCollection AddConcreteRepositories(this IServiceCollection services)
    {
        var repositoryInterfaces = typeof(IUnitOfWork).Assembly
            .GetTypes()
            .Where(x => x.IsInterface && x.Namespace == "Application.Abstractions" && x.Name.StartsWith('I') && x.Name.EndsWith("Repository"))
            .ToArray();

        var repositoryImplementations = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Repository", StringComparison.Ordinal))
            .ToArray();

        foreach (var repositoryInterface in repositoryInterfaces)
        {
            var implementation = repositoryImplementations.SingleOrDefault(x => repositoryInterface.IsAssignableFrom(x));
            if (implementation is not null)
            {
                services.AddScoped(repositoryInterface, implementation);
            }
        }

        return services;
    }
}
