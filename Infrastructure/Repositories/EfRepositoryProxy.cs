using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class EfRepositoryProxy : DispatchProxy
{
    private AppDbContext _context = null!;
    private Type _repositoryType = null!;

    public static object Create(Type repositoryType, AppDbContext context)
    {
        var createMethod = typeof(DispatchProxy)
            .GetMethod(nameof(DispatchProxy.Create), BindingFlags.Public | BindingFlags.Static)!
            .MakeGenericMethod(repositoryType, typeof(EfRepositoryProxy));

        var proxy = (EfRepositoryProxy)createMethod.Invoke(null, null)!;
        proxy._repositoryType = repositoryType;
        proxy._context = context;

        return proxy;
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        if (targetMethod is null)
        {
            throw new InvalidOperationException("No se pudo resolver el método del repositorio.");
        }

        args ??= Array.Empty<object?>();

        var entityType = ResolveEntityType(targetMethod, args);

        if (targetMethod.ReturnType == typeof(Task))
        {
            return InvokeAsync(targetMethod, entityType, args);
        }

        if (targetMethod.ReturnType.IsGenericType && targetMethod.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            var resultType = targetMethod.ReturnType.GetGenericArguments()[0];
            var invokeMethod = typeof(EfRepositoryProxy)
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Single(x => x.Name == nameof(InvokeAsync) && x.IsGenericMethod)
                .MakeGenericMethod(resultType);

            return invokeMethod.Invoke(this, new object[] { targetMethod, entityType, args });
        }

        throw new NotSupportedException($"El método {targetMethod.Name} debe retornar Task o Task<T>.");
    }

    private async Task InvokeAsync(MethodInfo method, Type entityType, object?[] args)
    {
        await InvokeRepositoryMethodAsync(method, entityType, args);
    }

    private async Task<T?> InvokeAsync<T>(MethodInfo method, Type entityType, object?[] args)
    {
        var result = await InvokeRepositoryMethodAsync(method, entityType, args);
        return result is null ? default : (T)result;
    }

    private async Task<object?> InvokeRepositoryMethodAsync(MethodInfo method, Type entityType, object?[] args)
    {
        var name = method.Name;

        if (name == "GetByIdAsync")
        {
            var id = (int)args[0]!;
            return await Query(entityType).FirstOrDefaultAsync(Equal(entityType, "Id", id), Cancellation(args));
        }

        if (name == "GetAllAsync")
        {
            return await ToTypedListAsync(Query(entityType).OrderBy(x => EF.Property<int>(x, "Id")), entityType, Cancellation(args));
        }

        if (name == "GetPagedAsync")
        {
            var page = (int)args[0]!;
            var pageSize = (int)args[1]!;
            var search = args[2] as string;
            var query = ApplySearch(Query(entityType), entityType, search);

            return await ToTypedListAsync(
                query.OrderBy(x => EF.Property<int>(x, "Id")).Skip((page - 1) * pageSize).Take(pageSize),
                entityType,
                Cancellation(args));
        }

        if (name == "CountAsync")
        {
            var search = args.Length > 0 ? args[0] as string : null;
            return await ApplySearch(Query(entityType), entityType, search).CountAsync(Cancellation(args));
        }

        if (name == "AddAsync")
        {
            await _context.AddAsync(args[0]!, Cancellation(args));
            return null;
        }

        if (name == "UpdateAsync")
        {
            _context.Update(args[0]!);
            return null;
        }

        if (name == "RemoveAsync")
        {
            _context.Remove(args[0]!);
            return null;
        }

        if (name == "ExistsAsync" && entityType == typeof(UserRole))
        {
            return await Query(entityType).AnyAsync(AndEqual(entityType, new[] { "UserId", "RoleId" }, args), Cancellation(args));
        }

        if (name == "GetByIdsAsync" && entityType == typeof(UserRole))
        {
            return await Query(entityType).FirstOrDefaultAsync(AndEqual(entityType, new[] { "UserId", "RoleId" }, args), Cancellation(args));
        }

        if (name == "HasActiveOrderForVehicleAsync")
        {
            return await Query(entityType).AnyAsync(AndEqual(entityType, new[] { "VehicleId" }, args), Cancellation(args));
        }

        if (name.StartsWith("Exists", StringComparison.Ordinal) && name.EndsWith("Async", StringComparison.Ordinal))
        {
            var properties = ExtractProperties(entityType, name, "Exists", "Async");
            return await Query(entityType).AnyAsync(AndEqual(entityType, properties, args), Cancellation(args));
        }

        if (name.StartsWith("GetBy", StringComparison.Ordinal) && name.EndsWith("Async", StringComparison.Ordinal))
        {
            var properties = ExtractProperties(entityType, name, "GetBy", "Async");
            var query = Query(entityType).Where(AndEqual(entityType, properties, args));
            var returnsList = IsListResult(method.ReturnType);

            if (returnsList)
            {
                return await ToTypedListAsync(query.OrderBy(x => EF.Property<int>(x, "Id")), entityType, Cancellation(args));
            }

            return await query.FirstOrDefaultAsync(Cancellation(args));
        }

        if (name == "GetLowStockAsync")
        {
            return await ToTypedListAsync(
                Query(entityType).Where(CompareProperties(entityType, "Stock", "MinimumStock", Expression.LessThanOrEqual)),
                entityType,
                Cancellation(args));
        }

        throw new NotSupportedException($"El método {method.Name} no está soportado por {nameof(EfRepositoryProxy)}.");
    }

    private IQueryable<object> Query(Type entityType)
    {
        var setMethod = typeof(DbContext)
            .GetMethods()
            .Single(x => x.Name == nameof(DbContext.Set) && x.IsGenericMethod && x.GetParameters().Length == 0)
            .MakeGenericMethod(entityType);

        return ((IQueryable)setMethod.Invoke(_context, null)!).Cast<object>();
    }

    private static Expression<Func<object, bool>> Equal(Type entityType, string propertyName, object? value)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var converted = Expression.Convert(parameter, entityType);
        var property = Expression.Property(converted, propertyName);
        var constant = Expression.Constant(CoerceValue(value, property.Type), property.Type);
        var body = Expression.Equal(property, constant);

        return Expression.Lambda<Func<object, bool>>(body, parameter);
    }

    private static Expression<Func<object, bool>> AndEqual(Type entityType, IReadOnlyList<string> propertyNames, object?[] args)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var converted = Expression.Convert(parameter, entityType);
        Expression? body = null;

        for (var i = 0; i < propertyNames.Count; i++)
        {
            var property = Expression.Property(converted, propertyNames[i]);
            var constant = Expression.Constant(CoerceValue(args[i], property.Type), property.Type);
            var equal = Expression.Equal(property, constant);
            body = body is null ? equal : Expression.AndAlso(body, equal);
        }

        return Expression.Lambda<Func<object, bool>>(body ?? Expression.Constant(true), parameter);
    }

    private static Expression<Func<object, bool>> CompareProperties(
        Type entityType,
        string leftPropertyName,
        string rightPropertyName,
        Func<Expression, Expression, BinaryExpression> comparison)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var converted = Expression.Convert(parameter, entityType);
        var left = Expression.Property(converted, leftPropertyName);
        var right = Expression.Property(converted, rightPropertyName);
        return Expression.Lambda<Func<object, bool>>(comparison(left, right), parameter);
    }

    private static IQueryable<object> ApplySearch(IQueryable<object> query, Type entityType, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        var searchableProperties = entityType
            .GetProperties()
            .Where(x => x.PropertyType == typeof(string))
            .ToArray();

        if (searchableProperties.Length == 0)
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(object), "x");
        var converted = Expression.Convert(parameter, entityType);
        Expression? body = null;
        var searchConstant = Expression.Constant(search.Trim());

        foreach (var searchableProperty in searchableProperties)
        {
            var property = Expression.Property(converted, searchableProperty.Name);
            var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
            var contains = Expression.Call(property, nameof(string.Contains), Type.EmptyTypes, searchConstant);
            var expression = Expression.AndAlso(notNull, contains);
            body = body is null ? expression : Expression.OrElse(body, expression);
        }

        return query.Where(Expression.Lambda<Func<object, bool>>(body!, parameter));
    }

    private static async Task<object> ToTypedListAsync(IQueryable<object> query, Type entityType, CancellationToken ct)
    {
        var items = await query.ToListAsync(ct);
        var listType = typeof(List<>).MakeGenericType(entityType);
        var list = (IList)Activator.CreateInstance(listType)!;

        foreach (var item in items)
        {
            list.Add(item);
        }

        return list;
    }

    private Type ResolveEntityType(MethodInfo method, object?[] args)
    {
        if (args.Length > 0 && args[0]?.GetType().Namespace == "Domain.Entities")
        {
            return args[0]!.GetType();
        }

        var returnType = method.ReturnType;
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            var taskResultType = returnType.GetGenericArguments()[0];
            var listType = GetListElementType(taskResultType);
            if (listType is not null)
            {
                return listType;
            }

            if (taskResultType.Namespace == "Domain.Entities")
            {
                return taskResultType;
            }
        }

        var entityName = _repositoryType.Name.TrimStart('I').Replace("Repository", string.Empty, StringComparison.Ordinal);
        return typeof(BaseEntity).Assembly.GetTypes().Single(x => x.Namespace == "Domain.Entities" && x.Name == entityName);
    }

    private static Type? GetListElementType(Type type)
    {
        if (!type.IsGenericType)
        {
            return null;
        }

        var genericTypeDefinition = type.GetGenericTypeDefinition();
        if (genericTypeDefinition == typeof(IReadOnlyList<>) || genericTypeDefinition == typeof(List<>))
        {
            return type.GetGenericArguments()[0];
        }

        return null;
    }

    private static bool IsListResult(Type returnType)
    {
        return returnType.IsGenericType
            && returnType.GetGenericTypeDefinition() == typeof(Task<>)
            && GetListElementType(returnType.GetGenericArguments()[0]) is not null;
    }

    private static IReadOnlyList<string> ExtractProperties(Type entityType, string methodName, string prefix, string suffix)
    {
        var body = methodName[prefix.Length..^suffix.Length];
        return body.Split("And", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => ResolvePropertyName(entityType, x))
            .ToArray();
    }

    private static string ResolvePropertyName(Type entityType, string name)
    {
        if (entityType.GetProperty(name) is not null)
        {
            return name;
        }

        var idName = $"{name}Id";
        if (entityType.GetProperty(idName) is not null)
        {
            return idName;
        }

        if (name == "Ids" && entityType == typeof(UserRole))
        {
            return "UserId";
        }

        return name;
    }

    private static object? CoerceValue(object? value, Type targetType)
    {
        if (value is null)
        {
            return null;
        }

        var valueProperty = value.GetType().GetProperty("Value");
        if (valueProperty is not null)
        {
            value = valueProperty.GetValue(value);
        }

        var nullableType = Nullable.GetUnderlyingType(targetType);
        if (nullableType is not null)
        {
            targetType = nullableType;
        }

        return targetType.IsEnum ? Enum.ToObject(targetType, value!) : Convert.ChangeType(value, targetType);
    }

    private static CancellationToken Cancellation(object?[] args)
    {
        return args.OfType<CancellationToken>().FirstOrDefault();
    }
}
