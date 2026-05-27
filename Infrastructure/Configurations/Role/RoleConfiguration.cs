using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Role;

public class RoleConfiguration : IEntityTypeConfiguration<Domain.Entities.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Role> entity)
    {
        entity.ToTable("Roles");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("RoleId");
        entity.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.RoleName).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
