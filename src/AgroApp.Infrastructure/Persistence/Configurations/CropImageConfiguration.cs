using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class CropImageConfiguration : IEntityTypeConfiguration<CropImage>
{
    public void Configure(EntityTypeBuilder<CropImage> builder)
    {
        builder.ToTable("crop_images");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CropId)
            .HasColumnName("crop_id");
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(x => x.StorageKey)
            .HasColumnName("storage_key")
            .HasMaxLength(500);
        builder.Property(x => x.Category)
            .HasMaxLength(100);
        builder.Property(x => x.AiDiagnosis)
            .HasColumnName("ai_diagnosis");
        builder.Property(x => x.AiAnalyzedAt)
            .HasColumnName("ai_analyzed_at");
        builder.Property(x => x.AiConfidence)
            .HasColumnName("ai_confidence");
        builder.Property(x => x.TakenAt)
            .HasColumnName("taken_at");
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.HasOne(x => x.Crop)
            .WithMany()
            .HasForeignKey(x => x.CropId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}