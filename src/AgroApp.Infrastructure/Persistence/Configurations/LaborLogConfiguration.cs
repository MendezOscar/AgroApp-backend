using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations
{
    public class LaborLogConfiguration : IEntityTypeConfiguration<LaborLog>
    {
        public void Configure(EntityTypeBuilder<LaborLog> builder)
        {
            builder.ToTable("labor_logs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ActivityType).IsRequired().HasMaxLength(100);
            builder.Property(x => x.HoursWorked).HasPrecision(6, 2);
            builder.Property(x => x.Cost).HasPrecision(12, 2);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");

            builder.HasOne(x => x.Crop).WithMany(x => x.LaborLogs)
                .HasForeignKey(x => x.CropId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany()
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}