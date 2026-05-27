using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicSpecialty;

public class MechanicSpecialtyConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicSpecialty>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicSpecialty> entity)
    {
        entity.ToTable("MechanicSpecialties");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("SpecialtyId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
