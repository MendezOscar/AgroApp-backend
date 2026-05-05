using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Sensors.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Sensors.Commands;

public class CreateSensorReadingCommandHandler : IRequestHandler<CreateSensorReadingCommand, SensorReadingDto>
{
    private readonly IApplicationDbContext _context;

    public CreateSensorReadingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SensorReadingDto> Handle(CreateSensorReadingCommand request, CancellationToken cancellationToken)
    {
        // Buscar dispositivo
        var device = await _context.SensorDevices
            .Include(d => d.Plot.Farm)
            .FirstOrDefaultAsync(d => d.Id == request.DeviceId && d.IsActive, cancellationToken);

        if (device is null)
            throw new InvalidOperationException("Dispositivo no encontrado.");

        // Guardar lectura
        var reading = new SensorReading
        {
            DeviceId = request.DeviceId,
            Temperature = request.Temperature,
            HumidityAir = request.HumidityAir,
            HumiditySoil = request.HumiditySoil,
            Luminosity = request.Luminosity,
            RainMm = request.RainMm,
            Ph = request.Ph,
            Ec = request.Ec,
            RecordedAt = DateTime.UtcNow
        };

        _context.SensorReadings.Add(reading);

        // Actualizar último ping del dispositivo
        device.LastSeenAt = DateTime.UtcNow;

        // Evaluar reglas de alerta
        var tenantId = device.Plot.Farm.TenantId;
        var rules = await _context.AlertRules
            .Where(r => r.TenantId == tenantId
                     && r.IsActive
                     && (r.PlotId == null || r.PlotId == device.PlotId))
            .ToListAsync(cancellationToken);

        foreach (var rule in rules)
        {
            var value = rule.Metric switch
            {
                "temperature" => request.Temperature,
                "humidity_air" => request.HumidityAir,
                "humidity_soil" => request.HumiditySoil,
                "luminosity" => request.Luminosity,
                "rain_mm" => request.RainMm,
                "ph" => request.Ph,
                "ec" => request.Ec,
                _ => null
            };

            if (value is null) continue;

            var triggered = rule.Operator switch
            {
                "lt" => value < rule.Threshold,
                "lte" => value <= rule.Threshold,
                "gt" => value > rule.Threshold,
                "gte" => value >= rule.Threshold,
                _ => false
            };

            if (triggered)
            {
                var alert = new Alert
                {
                    DeviceId = device.Id,
                    TenantId = tenantId,
                    PlotId = device.PlotId,
                    AlertType = rule.Metric,
                    Severity = Enum.Parse<Domain.Enums.AlertSeverity>(rule.Severity, true),
                    Message = $"{rule.Metric} es {value} (umbral: {rule.Operator} {rule.Threshold})",
                    TriggeredAt = DateTime.UtcNow
                };

                _context.Alerts.Add(alert);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new SensorReadingDto(
            reading.Id, reading.DeviceId, reading.Temperature,
            reading.HumidityAir, reading.HumiditySoil, reading.Luminosity,
            reading.RainMm, reading.Ph, reading.Ec, reading.RecordedAt);
    }
}