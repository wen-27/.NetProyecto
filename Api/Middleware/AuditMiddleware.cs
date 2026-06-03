using System.Security.Claims;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Middleware;

// Middleware que se ejecuta dentro de la tuberia HTTP para aplicar una preocupacion transversal.
public sealed class AuditMiddleware
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    private readonly RequestDelegate _next;

    public AuditMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        await _next(context);

        if (context.Request.Method == HttpMethods.Get || context.Response.StatusCode >= 400)
        {
            return;
        }

        var userIdValue = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdValue, out var userId))
        {
            return;
        }

        var actionName = context.Request.Method switch
        {
            "POST" => "CREATE",
            "PUT" or "PATCH" => "UPDATE",
            "DELETE" => "DELETE",
            _ => "UPDATE"
        };

        var actionTypeId = await dbContext.AuditActionTypes
            .Where(x => x.Name == actionName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(context.RequestAborted);

        if (actionTypeId == 0)
        {
            return;
        }

        dbContext.Audits.Add(new Audit
        {
            UserId = userId,
            AuditActionTypeId = actionTypeId,
            AffectedEntity = context.Request.Path.Value?.Trim('/').Split('/').LastOrDefault() ?? "Unknown",
            AffectedRecordId = TryReadAffectedRecordId(context),
            Description = $"{context.Request.Method} {context.Request.Path}",
            CreatedAt = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync(context.RequestAborted);
    }

    private static int TryReadAffectedRecordId(HttpContext context)
    {
        if (context.Request.RouteValues.TryGetValue("id", out var id) && int.TryParse(id?.ToString(), out var parsedId))
        {
            return parsedId;
        }

        if (context.Response.Headers.Location.Count > 0)
        {
            var location = context.Response.Headers.Location.ToString();
            var lastSegment = location.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (int.TryParse(lastSegment, out var createdId))
            {
                return createdId;
            }
        }

        return 0;
    }
}
