// Responsabilidad: Punto de entrada de la API; registra servicios, middlewares, Swagger, seguridad, CORS, rate limit y rutas HTTP.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Api.Extensions;
using Api.Middleware;
using Application;
using Infrastructure;
using Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddCorsService(builder.Configuration);
builder.Services.AddMapsterService();
builder.Services.AddRateLimitService();
builder.Services.AddJwtAuthService(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.Services.SeedDevelopmentDataAsync(builder.Configuration);
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(CorsServiceExtensions.PolicyName);
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuditMiddleware>();
app.MapControllers();
app.Run();
