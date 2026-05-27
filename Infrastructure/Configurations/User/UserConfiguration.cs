using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> entity)
    {
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
        entity.Ignore(x => x.ServiceOrderServicesAsMechanic);
        entity.Ignore(x => x.UpdatedAt);
    }
}
