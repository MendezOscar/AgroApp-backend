using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations
{
    public class IrrigationLogConfiguration : IEntityTypeConfiguration<IrrigationLog>
    {
        public void Configure(EntityTypeBuilder<IrrigationLog> builder)
        {
            builder.ToTable("irrigation_logs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Method).IsRequired().HasMaxLength(100);
            builder.Property(x => x.VolumeLiters).HasPrecision(10, 2);
            builder.Property(x => x.Cost).HasPrecision(12, 2);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");

            builder.HasOne(x => x.Crop).WithMany(x => x.IrrigationLogs)
                .HasForeignKey(x => x.CropId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany()
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Task).WithMany()
                .HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}