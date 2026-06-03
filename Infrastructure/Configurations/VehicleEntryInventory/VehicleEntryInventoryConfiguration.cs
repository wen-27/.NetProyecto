using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleEntryInventory;

// Configuracion de EF Core que mapea VehicleEntryInventory a tablas, columnas, relaciones e indices.
public class VehicleEntryInventoryConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleEntryInventory>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleEntryInventory> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
