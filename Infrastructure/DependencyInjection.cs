using Application.Abstractions;
using Application.Abstractions.OperationalWorkflow;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

// Tipo de backend que concentra una responsabilidad concreta dentro de la solucion.
public static class DependencyInjection
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySql");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "ConnectionStrings:MySql must be configured. Use user-secrets locally or ConnectionStrings__MySql in the environment.");
        }

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            );

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
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
