using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Alerts.Commands;

public class GenerateWeatherAlertsCommandHandler(
    IApplicationDbContext context,
    IWeatherService weatherService,
    INotificationService notifications)
        : IRequestHandler<GenerateWeatherAlertsCommand, int>
{
    private const string FrostAlertType = "frost_warning";
    private const string RainAlertType = "rain_suspend_irrigation";
    private static readonly TimeSpan DedupWindow = TimeSpan.FromHours(12);

    public async Task<int> Handle(
        GenerateWeatherAlertsCommand request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var farms = await context.Farms
            .Where(f => f.IsActive && f.Lat != null && f.Lng != null)
            .Select(f => new { f.Id, f.TenantId, Lat = f.Lat!.Value, Lng = f.Lng!.Value })
            .ToListAsync(cancellationToken);

        if (farms.Count == 0) return 0;

        var activeCropsByFarm = (await context.Crops
            .Where(c => c.Status == CropStatus.Active)
            .Select(c => new
            {
                CropId = c.Id,
                c.CropType,
                c.PlotId,
                FarmId = c.Plot.FarmId,
            })
            .ToListAsync(cancellationToken))
            .GroupBy(c => c.FarmId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var since = now - DedupWindow;
        var recentAlerts = (await context.Alerts
            .Where(a => (a.AlertType == FrostAlertType || a.AlertType == RainAlertType)
                     && a.PlotId != null && a.TriggeredAt >= since)
            .Select(a => new { a.PlotId, a.AlertType })
            .ToListAsync(cancellationToken))
            .Select(a => (a.PlotId!.Value, a.AlertType))
            .ToHashSet();

        var created = 0;

        foreach (var farm in farms)
        {
            if (!activeCropsByFarm.TryGetValue(farm.Id, out var crops) || crops.Count == 0)
                continue;

            var forecast = await weatherService.GetForecastAsync(farm.Lat, farm.Lng);
            if (forecast is null) continue;

            var frost = forecast.MinTemperatureC < request.FrostThresholdC;
            var rain = forecast.MaxPrecipitationMm is not null
                       && forecast.MaxPrecipitationMm >= request.RainThresholdMm;

            if (!frost && !rain) continue;

            foreach (var crop in crops)
            {
                if (frost && !recentAlerts.Contains((crop.PlotId, FrostAlertType)))
                {
                    var message = $"Riesgo de helada: mínima pronosticada {forecast.MinTemperatureC:0.#}°C para el cultivo {crop.CropType}.";
                    context.Alerts.Add(new Alert
                    {
                        TenantId = farm.TenantId,
                        PlotId = crop.PlotId,
                        CropId = crop.CropId,
                        AlertType = FrostAlertType,
                        Severity = AlertSeverity.Critical,
                        Message = message,
                        TriggeredAt = now,
                    });
                    await notifications.SendToTenantAsync(farm.TenantId,
                        title: "🥶 Riesgo de helada",
                        body: message,
                        data: new Dictionary<string, string>
                        {
                            ["plotId"] = crop.PlotId.ToString(),
                            ["type"] = FrostAlertType,
                        });
                    created++;
                }

                if (rain && !recentAlerts.Contains((crop.PlotId, RainAlertType)))
                {
                    var message = $"Se pronostican {forecast.MaxPrecipitationMm:0.#}mm de lluvia — considera suspender el riego de {crop.CropType}.";
                    context.Alerts.Add(new Alert
                    {
                        TenantId = farm.TenantId,
                        PlotId = crop.PlotId,
                        CropId = crop.CropId,
                        AlertType = RainAlertType,
                        Severity = AlertSeverity.Warning,
                        Message = message,
                        TriggeredAt = now,
                    });
                    await notifications.SendToTenantAsync(farm.TenantId,
                        title: "🌧️ Lluvia pronosticada",
                        body: message,
                        data: new Dictionary<string, string>
                        {
                            ["plotId"] = crop.PlotId.ToString(),
                            ["type"] = RainAlertType,
                        });
                    created++;
                }
            }
        }

        if (created > 0)
            await context.SaveChangesAsync(cancellationToken);

        return created;
    }
}
