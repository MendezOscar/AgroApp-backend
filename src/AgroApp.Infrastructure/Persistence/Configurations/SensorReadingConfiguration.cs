using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AgroApp.Infrastructure.Persistence.Configurations
{
    public class SensorReadingConfiguration : IEntityTypeConfiguration<SensorReading>
    {
        public void Configure(EntityTypeBuilder<SensorReading> builder)
        {
            builder.ToTable("sensor_readings");
            builder.HasKey(x => new { x.Id, x.RecordedAt });
            builder.Property(x => x.Temperature).HasPrecision(6, 2);
            builder.Property(x => x.HumidityAir).HasPrecision(6, 2);
            builder.Property(x => x.HumiditySoil).HasPrecision(6, 2);
            builder.Property(x => x.Luminosity).HasPrecision(10, 2);
            builder.Property(x => x.RainMm).HasPrecision(6, 2);
            builder.Property(x => x.Ph).HasPrecision(5, 2);
            builder.Property(x => x.Ec).HasPrecision(8, 2);
            builder.Property(x => x.RecordedAt).HasColumnName("recorded_at");

            builder.HasOne(x => x.Device)
                .WithMany(x => x.SensorReadings)
                .HasForeignKey(x => x.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}