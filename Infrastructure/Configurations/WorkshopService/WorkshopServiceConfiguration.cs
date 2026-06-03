// Responsabilidad: Configuracion de Entity Framework Core para mapear WorkshopService a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.WorkshopService;

public class WorkshopServiceConfiguration : IEntityTypeConfiguration<Domain.Entities.WorkshopService>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.WorkshopService> entity)
    {
        entity.ToTable("WorkshopServices");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("WorkshopServiceId");
        entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
        entity.Property(x => x.Description).HasColumnType("text");
        entity.Property(x => x.Category).HasMaxLength(80).IsRequired();
        entity.Property(x => x.LaborPercentage).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.PartsSubtotal).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.LaborAmount).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.FinalPrice).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.Status).HasConversion<int>().IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();
        entity.Property(x => x.IsActive).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
    }
}
