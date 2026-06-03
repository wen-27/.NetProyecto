// Responsabilidad: Configuracion de Entity Framework Core para mapear Address a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Address;

public class AddressConfiguration : IEntityTypeConfiguration<Domain.Entities.Address>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Address> entity)
    {
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
