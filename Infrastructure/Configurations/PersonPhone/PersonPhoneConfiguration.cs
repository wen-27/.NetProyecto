using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonPhone;

// Configuracion de EF Core que mapea PersonPhone a tablas, columnas, relaciones e indices.
public class PersonPhoneConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonPhone>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonPhone> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
