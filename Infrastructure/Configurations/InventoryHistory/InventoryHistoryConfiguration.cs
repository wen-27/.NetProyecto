using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.InventoryHistory;

public class InventoryHistoryConfiguration : IEntityTypeConfiguration<Domain.Entities.InventoryHistory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.InventoryHistory> entity)
    {
        entity.ToTable("InventoryHistory");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("InventoryHistoryId");
        entity.Property(x => x.UnitPrice).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.Action).HasMaxLength(80).IsRequired();
        entity.Property(x => x.Comment).HasColumnType("text");
        entity.Property(x => x.CreatedAt).IsRequired();

        entity.HasOne(x => x.Part)
            .WithMany(x => x.InventoryHistory)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.StockSubmission)
            .WithMany()
            .HasForeignKey(x => x.StockSubmissionId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
