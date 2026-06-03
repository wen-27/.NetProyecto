using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonEmail;

// Configuracion de EF Core que mapea PersonEmail a tablas, columnas, relaciones e indices.
public class PersonEmailConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonEmail>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonEmail> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("PersonEmails");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonEmailId");
        entity.Property(x => x.EmailUser).HasMaxLength(100).IsRequired();
        entity.Property(x => x.IsPrimary).IsRequired();
        entity.HasIndex(x => new { x.EmailUser, x.EmailDomainId }).IsUnique();

        entity.HasOne(x => x.Person)
            .WithMany(x => x.Emails)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.EmailDomain)
            .WithMany(x => x.PersonEmails)
            .HasForeignKey(x => x.EmailDomainId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
