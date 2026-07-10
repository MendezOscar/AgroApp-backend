using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class SoilAnalysisConfiguration : IEntityTypeConfiguration<SoilAnalysis>
{
    public void Configure(EntityTypeBuilder<SoilAnalysis> builder)
    {
        builder.ToTable("soil_analyses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Ph).HasPrecision(4, 2);
        builder.Property(x => x.NitrogenPct).HasPrecision(5, 2);
        builder.Property(x => x.PhosphorusPct).HasPrecision(5, 2);
        builder.Property(x => x.PotassiumPct).HasPrecision(5, 2);
        builder.Property(x => x.OrganicMatterPct).HasPrecision(5, 2);
        builder.Property(x => x.AnalyzedAt).HasColumnName("analyzed_at");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");

        builder.HasOne(x => x.Plot).WithMany(x => x.SoilAnalyses)
            .HasForeignKey(x => x.PlotId).OnDelete(DeleteBehavior.Cascade);
    }
}
