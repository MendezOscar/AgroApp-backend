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
            .HasColumnName("ai_confidence")
            .HasColumnType("real"); ;
        builder.Property(x => x.AiAnalyzedAt)
            .HasColumnName("ai_analyzed_at");
        builder.Property(x => x.DiagnosisCondition)
            .HasMaxLength(100)
            .HasColumnName("diagnosis_condition");
        builder.Property(x => x.TakenAt)
            .HasColumnName("taken_at");
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        // ← Clave: WithMany apunta a la colección en Crop
        builder.HasOne(x => x.Crop)
            .WithMany(c => c.CropImages)  // ← esto es lo que faltaba
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