using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.UserRole;

public class UserRoleConfiguration : IEntityTypeConfiguration<Domain.Entities.UserRole>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.UserRole> entity)
    {
        entity.ToTable("UserRoles");
        entity.HasKey(x => new { x.UserId, x.RoleId });

        entity.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
