using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Vehicle;

// Configuracion de EF Core que mapea Vehicle a tablas, columnas, relaciones e indices.
public class VehicleConfiguration : IEntityTypeConfiguration<Domain.Entities.Vehicle>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Vehicle> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Vehicles");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("VehicleId");
        entity.Property(x => x.Plate).HasMaxLength(10).IsRequired();
        entity.Property(x => x.Vin).HasColumnName("VIN").HasMaxLength(17).IsRequired();
        entity.Property(x => x.Year).IsRequired();
        entity.Property(x => x.Color).HasMaxLength(30);
        entity.Property(x => x.Mileage).IsRequired();
        entity.Property(x => x.IsActive).IsRequired();
        entity.HasIndex(x => x.Plate).IsUnique();
        entity.HasIndex(x => x.Vin).IsUnique();

        entity.HasOne(x => x.VehicleModel)
            .WithMany(x => x.Vehicles)
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.VehicleType)
            .WithMany(x => x.Vehicles)
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
    }
}
