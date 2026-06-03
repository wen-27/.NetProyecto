// Responsabilidad: Implementacion de repositorio para persistencia y consultas de GenericRepository; encapsula acceso a DbContext y detalles de EF Core.
// Nota de mantenimiento: Debe evitar reglas de negocio; su responsabilidad principal es consultar y persistir datos.
using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AppDbContext Context;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
    }

    public Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IReadOnlyList<TEntity>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        return await ApplySearch(Context.Set<TEntity>().AsNoTracking(), search)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        return ApplySearch(Context.Set<TEntity>().AsNoTracking(), search).CountAsync(ct);
    }

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, ct);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        Context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(TEntity entity, CancellationToken ct = default)
    {
        Context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await Context.Set<TEntity>().AsNoTracking().OrderBy(x => x.Id).ToListAsync(ct);
    }

    protected Task<TEntity?> FirstByAsync(string propertyName, object value, CancellationToken ct = default)
    {
        return Context.Set<TEntity>().FirstOrDefaultAsync(Equal(propertyName, value), ct);
    }

    protected async Task<IReadOnlyList<TEntity>> ListByAsync(string propertyName, object value, CancellationToken ct = default)
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .Where(Equal(propertyName, value))
            .OrderBy(x => x.Id)
            .ToListAsync(ct);
    }

    protected Task<bool> ExistsByAsync(string propertyName, object value, CancellationToken ct = default)
    {
        return Context.Set<TEntity>().AnyAsync(Equal(propertyName, value), ct);
    }

    protected Task<TEntity?> FirstByAsync(string firstProperty, object firstValue, string secondProperty, object secondValue, CancellationToken ct = default)
    {
        return Context.Set<TEntity>().FirstOrDefaultAsync(AndEqual(firstProperty, firstValue, secondProperty, secondValue), ct);
    }

    protected Task<bool> ExistsByAsync(string firstProperty, object firstValue, string secondProperty, object secondValue, CancellationToken ct = default)
    {
        return Context.Set<TEntity>().AnyAsync(AndEqual(firstProperty, firstValue, secondProperty, secondValue), ct);
    }

    protected static object ValueOf(object value)
    {
        return value.GetType().GetProperty("Value")?.GetValue(value) ?? value;
    }

    private static Expression<Func<TEntity, bool>> Equal(string propertyName, object value)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(ConvertValue(value, property.Type), property.Type);
        return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(property, constant), parameter);
    }

    private static Expression<Func<TEntity, bool>> AndEqual(string firstProperty, object firstValue, string secondProperty, object secondValue)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var first = Expression.Property(parameter, firstProperty);
        var second = Expression.Property(parameter, secondProperty);
        var firstConstant = Expression.Constant(ConvertValue(firstValue, first.Type), first.Type);
        var secondConstant = Expression.Constant(ConvertValue(secondValue, second.Type), second.Type);
        var body = Expression.AndAlso(Expression.Equal(first, firstConstant), Expression.Equal(second, secondConstant));
        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }

    private static object? ConvertValue(object value, Type targetType)
    {
        var rawValue = ValueOf(value);
        var nullableType = Nullable.GetUnderlyingType(targetType);
        if (nullableType is not null)
        {
            targetType = nullableType;
        }

        return targetType.IsEnum ? Enum.ToObject(targetType, rawValue) : Convert.ChangeType(rawValue, targetType);
    }

    private static IQueryable<TEntity> ApplySearch(IQueryable<TEntity> query, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        var stringProperties = typeof(TEntity)
            .GetProperties()
            .Where(x => x.PropertyType == typeof(string))
            .ToArray();

        if (stringProperties.Length == 0)
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        Expression? body = null;
        var searchExpression = Expression.Constant(search.Trim());

        foreach (var propertyInfo in stringProperties)
        {
            var property = Expression.Property(parameter, propertyInfo.Name);
            var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
            var contains = Expression.Call(property, nameof(string.Contains), Type.EmptyTypes, searchExpression);
            var expression = Expression.AndAlso(notNull, contains);
            body = body is null ? expression : Expression.OrElse(body, expression);
        }

        return query.Where(Expression.Lambda<Func<TEntity, bool>>(body!, parameter));
    }
}
