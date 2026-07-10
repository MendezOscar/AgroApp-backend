using AgroApp.Application.Features.Alerts.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgroApp.Infrastructure.Services;

/// <summary>
/// Genera alertas de helada y de lluvia pronosticada (sugerencia de
/// suspender riego) para cada finca con coordenadas, cada 12 horas.
/// </summary>
public class WeatherAlertBackgroundService(
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<WeatherAlertBackgroundService> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromHours(12);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var frostThreshold = configuration.GetValue("Weather:FrostThresholdC", 2m);
        var rainThreshold = configuration.GetValue("Weather:RainThresholdMm", 10m);

        using var timer = new PeriodicTimer(Interval);
        do
        {
            await RunOnceAsync(frostThreshold, rainThreshold, stoppingToken);
        } while (!stoppingToken.IsCancellationRequested
                 && await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task RunOnceAsync(
        decimal frostThreshold, decimal rainThreshold, CancellationToken cancellationToken)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var created = await mediator.Send(
                new GenerateWeatherAlertsCommand(frostThreshold, rainThreshold), cancellationToken);

            if (created > 0)
                logger.LogInformation("Generadas {Count} alertas de clima", created);
        }
        catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogError(ex, "Error generando alertas de clima");
        }
    }
}
