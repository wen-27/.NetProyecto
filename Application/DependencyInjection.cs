using Application.Common;
using Application.Common.Pagination;
using Application.UseCase.CommonCrud;
using Domain.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

// Tipo de backend que concentra una responsabilidad concreta dentro de la solucion.
public static class DependencyInjection
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
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
