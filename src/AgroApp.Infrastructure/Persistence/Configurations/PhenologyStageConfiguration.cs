using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class PhenologyStageConfiguration
    : IEntityTypeConfiguration<PhenologyStage>
{
    public void Configure(EntityTypeBuilder<PhenologyStage> builder)
    {
        builder.ToTable("phenology_stages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CropId)
            .HasColumnName("crop_id").IsRequired();
        builder.Property(x => x.TenantId)
            .HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TemplateId)
            .HasColumnName("template_id");
        builder.Property(x => x.StageName)
            .IsRequired().HasMaxLength(100)
            .HasColumnName("stage_name");
        builder.Property(x => x.StageOrder)
            .HasColumnName("stage_order");
        builder.Property(x => x.StartedAt)
            .HasColumnName("started_at");
        builder.Property(x => x.EndedAt)
            .HasColumnName("ended_at");
        builder.Property(x => x.Observations)
            .HasColumnName("observations");
        builder.Property(x => x.IsCustom)
            .HasColumnName("is_custom");
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasOne(x => x.Crop)
            .WithMany(c => c.PhenologyStages)
            .HasForeignKey(x => x.CropId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Template)
            .WithMany()
            .HasForeignKey(x => x.TemplateId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}