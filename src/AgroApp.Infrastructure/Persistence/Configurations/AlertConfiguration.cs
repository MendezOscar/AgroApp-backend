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
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.TenantId).HasColumnName("tenant_id");
        builder.Property(x => x.DeviceId).HasColumnName("device_id");
        builder.Property(x => x.PlotId).HasColumnName("plot_id");
        builder.Property(x => x.AlertType).IsRequired().HasMaxLength(100).HasColumnName("alert_type");
        builder.Property(x => x.Severity).HasConversion<string>().HasMaxLength(50).HasColumnName("severity");
        builder.Property(x => x.Message).IsRequired().HasColumnName("message");
        builder.Property(x => x.IsRead).HasColumnName("is_read").HasDefaultValue(false);
        builder.Property(x => x.TriggeredAt).HasColumnName("triggered_at");
        builder.Property(x => x.ReadAt).HasColumnName("read_at");

        builder.HasOne(x => x.Tenant).WithMany()
            .HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Device).WithMany(x => x.Alerts)
            .HasForeignKey(x => x.DeviceId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Plot).WithMany()
            .HasForeignKey(x => x.PlotId).OnDelete(DeleteBehavior.SetNull);
    }
}