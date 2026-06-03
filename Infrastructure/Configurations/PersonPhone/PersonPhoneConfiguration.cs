// Responsabilidad: Configuracion de Entity Framework Core para mapear PersonPhone a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonPhone;

public class PersonPhoneConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonPhone>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonPhone> entity)
    {
        entity.ToTable("PersonPhones");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonPhoneId");
        entity.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
        entity.Property(x => x.IsPrimary).IsRequired();
        entity.HasIndex(x => new { x.CountryId, x.PhoneNumber }).IsUnique();

        entity.HasOne(x => x.Person)
            .WithMany(x => x.Phones)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
