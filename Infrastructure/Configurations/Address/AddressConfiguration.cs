using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Address;

// Configuracion de EF Core que mapea Address a tablas, columnas, relaciones e indices.
public class AddressConfiguration : IEntityTypeConfiguration<Domain.Entities.Address>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Address> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Addresses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("AddressId");
        entity.Property(x => x.MainNumber).HasMaxLength(10);
        entity.Property(x => x.SecondaryNumber).HasMaxLength(10);
        entity.Property(x => x.TertiaryNumber).HasMaxLength(10);
        entity.Property(x => x.Complement).HasMaxLength(150);
        entity.HasOne(x => x.Neighborhood).WithMany().HasForeignKey(x => x.NeighborhoodId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.StreetType).WithMany().HasForeignKey(x => x.StreetTypeId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
