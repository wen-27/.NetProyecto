// Responsabilidad: Configuracion de Entity Framework Core para mapear VehicleEntryInventory a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleEntryInventory;

public class VehicleEntryInventoryConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleEntryInventory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleEntryInventory> entity)
    {
        entity.ToTable("VehicleEntryInventory");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("EntryInventoryId");
        entity.Property(x => x.ScratchesDescription).HasColumnType("text");
        entity.Property(x => x.ToolboxDescription).HasColumnType("text");
        entity.Property(x => x.Observations).HasColumnType("text");
        entity.Property(x => x.RegisteredAt).IsRequired();
        entity.HasIndex(x => x.ServiceOrderId).IsUnique();
        entity.HasOne(x => x.ServiceOrder).WithOne().HasForeignKey<Domain.Entities.VehicleEntryInventory>(x => x.ServiceOrderId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
