using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AgroApp.Infrastructure.Persistence.Configurations
{
    public class CropConfiguration : IEntityTypeConfiguration<Crop>
    {
        public void Configure(EntityTypeBuilder<Crop> builder)
        {
            builder.ToTable("crops");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CropType).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Variety).HasMaxLength(100);
            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50);
            builder.Property(x => x.YieldKg).HasPrecision(10, 2);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            builder.HasOne(x => x.Plot)
                .WithMany(x => x.Crops)
                .HasForeignKey(x => x.PlotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}