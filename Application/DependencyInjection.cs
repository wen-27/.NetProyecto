// Responsabilidad: Archivo de backend DependencyInjection; forma parte de la capa Application y participa en la estructura general de la aplicacion.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Application.Common;
using Application.Common.Pagination;
using Application.UseCase.CommonCrud;
using Domain.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ApplicationAssemblyReference).Assembly;

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddCommonCrudHandlers();

        return services;
    }

    private static IServiceCollection AddCommonCrudHandlers(this IServiceCollection services)
    {
        var entityTypes = typeof(BaseEntity).Assembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false } && type.IsAssignableTo(typeof(BaseEntity)))
            .ToArray();

        foreach (var entityType in entityTypes)
        {
            services.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    typeof(GetEntityById<>).MakeGenericType(entityType),
                    entityType),
                typeof(GetEntityByIdHandler<>).MakeGenericType(entityType));

            services.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    typeof(GetEntitiesPaged<>).MakeGenericType(entityType),
                    typeof(PagedResult<>).MakeGenericType(entityType)),
                typeof(GetEntitiesPagedHandler<>).MakeGenericType(entityType));

            services.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    typeof(CreateEntity<>).MakeGenericType(entityType),
                    typeof(int)),
                typeof(CreateEntityHandler<>).MakeGenericType(entityType));

            services.AddScoped(
                typeof(IRequestHandler<>).MakeGenericType(typeof(UpdateEntity<>).MakeGenericType(entityType)),
                typeof(UpdateEntityHandler<>).MakeGenericType(entityType));

            services.AddScoped(
                typeof(IRequestHandler<>).MakeGenericType(typeof(DeleteEntity<>).MakeGenericType(entityType)),
                typeof(DeleteEntityHandler<>).MakeGenericType(entityType));
        }

        return services;
    }
}
