using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Alerts.Commands;

public class GenerateIrrigationRemindersCommandHandler(
    IApplicationDbContext context,
    INotificationService notifications)
        : IRequestHandler<GenerateIrrigationRemindersCommand, int>
{
    private const string ReminderAlertType = "irrigation_reminder";

    private readonly IApplicationDbContext _context = context;
    private readonly INotificationService _notifications = notifications;

    public async Task<int> Handle(
        GenerateIrrigationRemindersCommand request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var activeCrops = await _context.Crops
            .Where(c => c.Status == CropStatus.Active)
            .Select(c => new
            {
                c.Id,
                c.CropType,
                c.PlantedAt,
                c.PlotId,
                c.Plot.Farm.TenantId
            })
            .ToListAsync(cancellationToken);

        var lastIrrigationByCrop = await _context.IrrigationLogs
            .GroupBy(i => i.CropId)
            .Select(g => new { CropId = g.Key, LastAppliedAt = g.Max(i => i.AppliedAt) })
            .ToDictionaryAsync(x => x.CropId, x => x.LastAppliedAt, cancellationToken);

        var since24h = now.AddHours(-24);
        var latestSoilHumidityByPlot = (await _context.SensorReadings
            .Where(r => r.RecordedAt >= since24h && r.HumiditySoil != null)
            .GroupBy(r => r.Device.PlotId)
            .Select(g => new
            {
                PlotId = g.Key,
                Humidity = g.OrderByDescending(r => r.RecordedAt).First().HumiditySoil
            })
            .ToListAsync(cancellationToken))
            .ToDictionary(x => x.PlotId, x => x.Humidity);

        var since = now.AddDays(-request.ThresholdDays);
        var recentlyRemindedCropIds = (await _context.Alerts
            .Where(a => a.AlertType == ReminderAlertType
                     && a.CropId != null
                     && a.TriggeredAt >= since)
            .Select(a => a.CropId!.Value)
            .ToListAsync(cancellationToken))
            .ToHashSet();

        var created = 0;

        foreach (var crop in activeCrops)
        {
            if (recentlyRemindedCropIds.Contains(crop.Id))
                continue;

            var baseline = lastIrrigationByCrop.TryGetValue(crop.Id, out var lastAppliedAt)
                ? lastAppliedAt
                : crop.PlantedAt.ToDateTime(TimeOnly.MinValue);

            var daysSince = (now - baseline).Days;
            var hasLowSoilHumidity =
                latestSoilHumidityByPlot.TryGetValue(crop.PlotId, out var soilHumidity)
                && soilHumidity < request.SoilHumidityThresholdPct;

            if (daysSince < request.ThresholdDays && !hasLowSoilHumidity)
                continue;

            var message = hasLowSoilHumidity
                ? $"Humedad de suelo baja ({soilHumidity:0.#}%) en el cultivo {crop.CropType}."
                : $"Hace {daysSince} días que no riegas el cultivo {crop.CropType}.";

            _context.Alerts.Add(new Alert
            {
                TenantId = crop.TenantId,
                CropId = crop.Id,
                AlertType = ReminderAlertType,
                Severity = AlertSeverity.Warning,
                Message = message,
                TriggeredAt = now,
            });

            await _notifications.SendToTenantAsync(
                crop.TenantId,
                title: "💧 Recordatorio de riego",
                body: message,
                data: new Dictionary<string, string>
                {
                    ["cropId"] = crop.Id.ToString(),
                    ["type"] = ReminderAlertType
                });

            created++;
        }

        if (created > 0)
            await _context.SaveChangesAsync(cancellationToken);

        return created;
    }
}
