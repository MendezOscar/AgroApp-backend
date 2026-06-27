using AgroApp.Application.Features.Alerts.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgroApp.Infrastructure.Services;

/// <summary>
/// Genera recordatorios de riego ("hace N días que no riegas el cultivo X")
/// una vez al día para todos los tenants.
/// </summary>
public class IrrigationReminderBackgroundService(
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<IrrigationReminderBackgroundService> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var thresholdDays = configuration.GetValue("Reminders:IrrigationThresholdDays", 7);

        using var timer = new PeriodicTimer(Interval);
        do
        {
            await RunOnceAsync(thresholdDays, stoppingToken);
        } while (!stoppingToken.IsCancellationRequested
                 && await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task RunOnceAsync(int thresholdDays, CancellationToken cancellationToken)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var created = await mediator.Send(
                new GenerateIrrigationRemindersCommand(thresholdDays), cancellationToken);

            if (created > 0)
                logger.LogInformation("Generados {Count} recordatorios de riego", created);
        }
        catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogError(ex, "Error generando recordatorios de riego");
        }
    }
}
