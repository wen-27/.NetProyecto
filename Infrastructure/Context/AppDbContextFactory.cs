using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

// Contexto o fabrica de EF Core que centraliza la conexion con la base de datos.
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public AppDbContext CreateDbContext(string[] args)
    {
        var apiSettingsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Api"));
        if (!Directory.Exists(apiSettingsPath))
        {
            apiSettingsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Api"));
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiSettingsPath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("MySql");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "ConnectionStrings:MySql must be configured in Api/appsettings.json.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 36)));

        return new AppDbContext(optionsBuilder.Options);
    }
}
