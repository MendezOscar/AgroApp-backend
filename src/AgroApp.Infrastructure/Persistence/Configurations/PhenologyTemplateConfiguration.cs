using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class PhenologyTemplateConfiguration
    : IEntityTypeConfiguration<PhenologyTemplate>
{
    public void Configure(EntityTypeBuilder<PhenologyTemplate> builder)
    {
        builder.ToTable("phenology_templates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CropType)
            .IsRequired().HasMaxLength(100)
            .HasColumnName("crop_type");
        builder.Property(x => x.StageName)
            .IsRequired().HasMaxLength(100)
            .HasColumnName("stage_name");
        builder.Property(x => x.StageOrder)
            .HasColumnName("stage_order");
        builder.Property(x => x.Description)
            .HasColumnName("description");
        builder.Property(x => x.MinDays)
            .HasColumnName("min_days");
        builder.Property(x => x.MaxDays)
            .HasColumnName("max_days");
        builder.Property(x => x.Icon)
            .HasMaxLength(10)
            .HasColumnName("icon");
        builder.Property(x => x.Recommendations)
            .HasColumnName("recommendations");
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");
    }
}