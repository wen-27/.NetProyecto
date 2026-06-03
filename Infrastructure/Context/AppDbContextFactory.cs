using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Context;

// Contexto o fabrica de EF Core que centraliza la conexion con la base de datos.
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MySql");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "ConnectionStrings__MySql must be set before running design-time EF commands.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 36)));

        return new AppDbContext(optionsBuilder.Options);
    }
}
