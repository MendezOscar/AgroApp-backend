using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations
{
    public class FertilizationLogConfiguration : IEntityTypeConfiguration<FertilizationLog>
    {
        public void Configure(EntityTypeBuilder<FertilizationLog> builder)
        {
            builder.ToTable("fertilization_logs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ProductType).HasMaxLength(100);
            builder.Property(x => x.Method).HasMaxLength(100);
            builder.Property(x => x.DoseKgHa).HasPrecision(10, 4);
            builder.Property(x => x.TotalKg).HasPrecision(10, 4);
            builder.Property(x => x.Cost).HasPrecision(12, 2);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");

            builder.HasOne(x => x.Crop).WithMany(x => x.FertilizationLogs)
                .HasForeignKey(x => x.CropId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany()
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Task).WithMany()
                .HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}