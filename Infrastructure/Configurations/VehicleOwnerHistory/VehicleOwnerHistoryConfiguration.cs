using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleOwnerHistory;

// Configuracion de EF Core que mapea VehicleOwnerHistory a tablas, columnas, relaciones e indices.
public class VehicleOwnerHistoryConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleOwnerHistory>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleOwnerHistory> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("VehicleOwnerHistory");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("VehicleOwnerHistoryId");
        entity.Property(x => x.StartDate).HasColumnType("date").IsRequired();
        entity.Property(x => x.EndDate).HasColumnType("date");

        entity.HasOne(x => x.Vehicle)
            .WithMany(x => x.OwnerHistory)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Person)
            .WithMany(x => x.VehicleHistory)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
