using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroApp.Infrastructure.Persistence.Configurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("alerts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AlertType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Severity).HasConversion<string>().HasMaxLength(50);
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x => x.TriggeredAt).HasColumnName("triggered_at");
        builder.Property(x => x.ReadAt).HasColumnName("read_at");
        builder.Ignore(x => x.IsRead); // Ya tiene nombre correcto con snake_case

        builder.HasOne(x => x.Tenant).WithMany()
            .HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Device).WithMany(x => x.Alerts)
            .HasForeignKey(x => x.DeviceId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Plot).WithMany()
            .HasForeignKey(x => x.PlotId).OnDelete(DeleteBehavior.SetNull);
    }
}