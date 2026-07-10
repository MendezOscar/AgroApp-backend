using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class CropSaleConfiguration : IEntityTypeConfiguration<CropSale>
{
    public void Configure(EntityTypeBuilder<CropSale> builder)
    {
        builder.ToTable("crop_sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.QuantityKg).HasPrecision(10, 2);
        builder.Property(x => x.PricePerKg).HasPrecision(10, 2);
        builder.Property(x => x.Buyer).HasMaxLength(200);
        builder.Property(x => x.SoldAt).HasColumnName("sold_at");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");

        builder.HasOne(x => x.Crop).WithMany(x => x.CropSales)
            .HasForeignKey(x => x.CropId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.User).WithMany()
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
