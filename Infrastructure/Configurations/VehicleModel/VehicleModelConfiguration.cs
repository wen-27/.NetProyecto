using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleModel;

// Configuracion de EF Core que mapea VehicleModel a tablas, columnas, relaciones e indices.
public class VehicleModelConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleModel>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleModel> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("VehicleModels");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ModelId");
        entity.Property(x => x.ModelName).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => new { x.BrandId, x.ModelName }).IsUnique();

        entity.HasOne(x => x.VehicleBrand)
            .WithMany(x => x.Models)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
