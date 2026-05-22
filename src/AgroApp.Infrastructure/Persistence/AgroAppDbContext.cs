using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Infrastructure.Persistence;

public class AgroAppDbContext(DbContextOptions<AgroAppDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Plot> Plots => Set<Plot>();
    public DbSet<Crop> Crops => Set<Crop>();
    public DbSet<PhenologyStage> PhenologyStages => Set<PhenologyStage>();
    public DbSet<IrrigationLog> IrrigationLogs => Set<IrrigationLog>();
    public DbSet<FertilizationLog> FertilizationLogs => Set<FertilizationLog>();
    public DbSet<LaborLog> LaborLogs => Set<LaborLog>();
    public DbSet<SensorDevice> SensorDevices => Set<SensorDevice>();
    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<AlertRule> AlertRules => Set<AlertRule>();
    public DbSet<CropImage> CropImages => Set<CropImage>();
    public DbSet<CostEntry> CostEntries => Set<CostEntry>();
    public DbSet<FcmToken> FcmTokens => Set<FcmToken>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<TaskTemplate> TaskTemplates => Set<TaskTemplate>();
    public DbSet<TaskOccurrence> TaskOccurrences => Set<TaskOccurrence>();
    public DbSet<PhenologyTemplate> PhenologyTemplates => Set<PhenologyTemplate>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgroAppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Domain.Common.BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}