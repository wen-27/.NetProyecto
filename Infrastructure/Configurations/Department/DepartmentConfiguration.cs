using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Department;

public class DepartmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Department>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Department> entity)
    {
        entity.ToTable("Departments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("DepartmentId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
