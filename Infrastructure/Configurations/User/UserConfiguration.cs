using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.User;

// Configuracion de EF Core que mapea User a tablas, columnas, relaciones e indices.
public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.User> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Users");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("UserId");
        entity.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
        entity.Property(x => x.RefreshToken).HasColumnType("text");
        entity.Property(x => x.RefreshTokenExpiration);
        entity.Property(x => x.IsActive).IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();
        entity.HasIndex(x => x.PersonId).IsUnique();

        entity.HasOne(x => x.Person)
            .WithOne(x => x.User)
            .HasForeignKey<Domain.Entities.User>(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.Status);
        entity.Ignore(x => x.UpdatedAt);
    }
}
