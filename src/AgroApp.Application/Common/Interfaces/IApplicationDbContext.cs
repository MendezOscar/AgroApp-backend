using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<User> Users { get; }
    DbSet<Farm> Farms { get; }
    DbSet<Plot> Plots { get; }
    DbSet<Crop> Crops { get; }
    DbSet<IrrigationLog> IrrigationLogs { get; }
    DbSet<FertilizationLog> FertilizationLogs { get; }
    DbSet<LaborLog> LaborLogs { get; }
    DbSet<SensorDevice> SensorDevices { get; }
    DbSet<SensorReading> SensorReadings { get; }
    DbSet<Alert> Alerts { get; }
    DbSet<AlertRule> AlertRules { get; }
    DbSet<CropImage> CropImages { get; }
    DbSet<CostEntry> CostEntries { get; }
    DbSet<FcmToken> FcmTokens { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}