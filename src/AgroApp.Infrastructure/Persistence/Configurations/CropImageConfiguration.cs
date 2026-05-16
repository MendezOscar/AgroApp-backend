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

        builder.Property(x => x.Id)
            .HasColumnName("id");
        builder.Property(x => x.CropId)
            .HasColumnName("crop_id")
            .IsRequired();
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("url");
        builder.Property(x => x.StorageKey)
            .HasColumnName("storage_key")
            .HasMaxLength(500);
        builder.Property(x => x.Category)
            .HasMaxLength(100)
            .HasColumnName("category");
        builder.Property(x => x.AiDiagnosis)
            .HasColumnName("ai_diagnosis");
        builder.Property(x => x.AiConfidence)
            .HasColumnName("ai_confidence");
        builder.Property(x => x.AiAnalyzedAt)
            .HasColumnName("ai_analyzed_at");
        builder.Property(x => x.TakenAt)
            .HasColumnName("taken_at");
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        // ← FK explícitas para evitar crop_id1 y user_id1
        builder.HasOne(x => x.Crop)
            .WithMany()
            .HasForeignKey(x => x.CropId)
            .HasConstraintName("fk_crop_images_crops")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_crop_images_users")
            .OnDelete(DeleteBehavior.Restrict);
    }
}